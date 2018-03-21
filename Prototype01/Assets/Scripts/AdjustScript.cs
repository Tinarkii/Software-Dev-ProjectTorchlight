using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AdjustScript : MonoBehaviour {

public GameObject canvas;
public GameObject button;

    void Start () {
		GameObject newCanvas = Instantiate(canvas) as GameObject;
        GameObject newButton1 = Instantiate(button) as GameObject;
		GameObject newButton2 = Instantiate(button) as GameObject;
        GameObject newButton3 = Instantiate(button) as GameObject;
        GameObject newButton4 = Instantiate(button) as GameObject;

		newButton1.GetComponent<GUIText> ().text = "Health Up";
        newButton1.transform.SetParent(newCanvas.transform, false);
		
		newButton2.GetComponent<GUIText> ().text = "Health Down";
        newButton2.transform.SetParent(newCanvas.transform, false);

		newButton3.GetComponent<GUIText> ().text = "Save";
        newButton3.transform.SetParent(newCanvas.transform, false);

		newButton4.GetComponent<GUIText> ().text = "Load";
        newButton4.transform.SetParent(newCanvas.transform, false);

    }

	void OnGUI()
	{
		if(!EventSystem.current.IsPointerOverGameObject())
		{
			if (GUI.Button(new Rect(10,100,100,30),"Health Up"))
			{
			GameControl.control.confidence += 10;
			}
			if (GUI.Button(new Rect(10,140,100,30),"Health Down"))
			{
				GameControl.control.confidence -= 10;
			}
			if (GUI.Button(new Rect(10,180,100,30),"Save"))
			{
				GameControl.control.Save();
			}
			if (GUI.Button(new Rect(10,220,100,30),"Load"))
			{
				GameControl.control.Load();
			}	
		}	
	}


	 void Update () {
        if (EventSystem.current.IsPointerOverGameObject ()) {
            // we're over a UI element... return 1
            Debug.Log("over");
            }
        else{
            Debug.Log("not over");
            }
    }
}
