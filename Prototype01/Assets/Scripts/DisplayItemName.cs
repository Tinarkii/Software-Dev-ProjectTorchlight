using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItemName : MonoBehaviour {
	Vector3 pos;
	Camera cam;
	bool entered;
	// Use this for initialization
	void Start () 
	{
		entered = false;	
	}
	private void OnMouseEnter() {
		entered = true;
	}
	/// <summary>
	/// Called when the mouse is not any longer over the GUIElement or Collider.
	/// </summary>
	void OnMouseExit()
	{
		entered = false;
	}
	void OnGUI()
     {
		if(entered)
		{
			GUI.enabled=true;
			if(Camera.current != null)
				cam = Camera.current;
			pos = cam.WorldToScreenPoint(new Vector3(0,5,0));
			GUI.Label(new Rect(pos.x - gameObject.transform.position.x - 30, Screen.height - pos.y, 150, 130), gameObject.name);
		}
     }
	
	// Update is called once per frame
	void Update () {
		
	}
}
