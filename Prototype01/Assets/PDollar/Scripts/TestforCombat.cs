using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class TestforCombat : MonoBehaviour {

	public Transform gestureOnScreenPrefab;

	private List<Gesture> trainingSet = new List<Gesture>();

	private List<Point> points = new List<Point>();
	private int strokeId = -1;

	private Vector3 virtualKeyPosition = Vector2.zero;
	private Rect drawArea;

	private RuntimePlatform platform;
	private int vertexCount = 0;

	private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	private LineRenderer currentGestureLineRenderer;

	//GUI
	private string message;
	private bool recognized;
	private string newGestureName = "";

	public string[] shapes = new string[4];
	public int attack;
	public int lastAttack;

	void Start () {

		platform = Application.platform;
		drawArea = new Rect(0, 0, Screen.width, Screen.height);

		//Load pre-made gestures
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/Shapes/");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

		//Load user custom gestures
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (string filePath in filePaths)
			trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
		
		
		shapes[0] = "X";
		shapes[1] = "O";
		shapes[2] = "Triangle";
		//shapes[3] = "M";
		StartCoroutine("NewAttack");
	}
	IEnumerator NewAttack() {
		for(;;) {
			if(lastAttack == null){
				attack = UnityEngine.Random.Range(0,3);
			}else if(attack == lastAttack){
				attack = UnityEngine.Random.Range(0,3);
			}else attack = UnityEngine.Random.Range(0,3);

			yield return new WaitForSeconds(4f);
			/*newGestureName = shapes[attack];
			if (points.Count > 0) {
				string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());
				trainingSet.Add(new Gesture(points.ToArray(), newGestureName));
				newGestureName = "";
			}*/


			recognized = true;
			Gesture candidate = new Gesture(points.ToArray());
			Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
			//if (gestureResult.Score < .6) message = "Attack Failed!";
			if (gestureResult.GestureClass.CompareTo(shapes[attack]) != 0) message = shapes[attack] + " Attack Failed!" + " (You tried " +  gestureResult.GestureClass + " Attack " + gestureResult.Score + ")";
			else message = shapes[attack] + " Attack Succeeded!" + " (" + gestureResult.Score + ")";
			recognized = false;
			strokeId = -1;
			points.Clear();
			foreach (LineRenderer lineRenderer in gestureLinesRenderer) {
				lineRenderer.SetVertexCount(0);
				Destroy(lineRenderer.gameObject);
			}
			gestureLinesRenderer.Clear();
        	
		}
	}

	void Update () {

		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
			if (Input.touchCount > 0) {
				virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		} else {
			if (Input.GetMouseButton(0)) {
				virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		if (drawArea.Contains(virtualKeyPosition)) {

			if (Input.GetMouseButtonDown(0)) {

				if (recognized) {

					recognized = false;
					strokeId = -1;

					points.Clear();

					foreach (LineRenderer lineRenderer in gestureLinesRenderer) {

						lineRenderer.SetVertexCount(0);
						Destroy(lineRenderer.gameObject);
					}

					gestureLinesRenderer.Clear();
				}

				++strokeId;
				
				Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
				currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
				
				gestureLinesRenderer.Add(currentGestureLineRenderer);
				
				vertexCount = 0;
			}
			
			if (Input.GetMouseButton(0)) {
				points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

				currentGestureLineRenderer.SetVertexCount(++vertexCount);
				currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
			}
		}
	}

	void OnGUI() {
  		GUI.color = new Color(1,1,1,0); 
    	GUI.Box (drawArea, "Draw Area");
		GUI.color = new Color(1,1,1,1);
		//GUI.Box(drawArea, "Draw Area");
		GUIStyle myStyle = new GUIStyle (GUI.skin.GetStyle("label"));
        
		myStyle.fontSize = 80;
		myStyle.normal.textColor = Color.black;
		GUI.Label(new Rect((Screen.width/2)-50, 50, 500, 500), shapes[attack], myStyle);

		myStyle.fontSize = 18;
		GUI.Label(new Rect(10, Screen.height - 40, 500, 500), message, myStyle);

		/*if (GUI.Button(new Rect((Screen.width/2)-50, Screen.height - 50, 100, 30), "Send Attack!")) {

			recognized = true;

			Gesture candidate = new Gesture(points.ToArray());
			Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
			
			if (gestureResult.Score < .7) message = "Attack Failed!" + " (" +  gestureResult.GestureClass + " " + gestureResult.Score + ")";
			else message = gestureResult.GestureClass + " Attack Succeeded!" + " (" + gestureResult.Score + ")";
		}

		GUI.Label(new Rect(Screen.width - 200, 150, 70, 30), "Add as: ");
		newGestureName = GUI.TextField(new Rect(Screen.width - 150, 150, 100, 30), newGestureName);

		if (GUI.Button(new Rect(Screen.width - 50, 150, 50, 30), "Add") && points.Count > 0 && newGestureName != "") {

			string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

			#if !UNITY_WEBPLAYER
				GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
			#endif

			trainingSet.Add(new Gesture(points.ToArray(), newGestureName));

			newGestureName = "";
		}*/
	}
}
