using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorStuff : MonoBehaviour {
 
	// Use this for initialization
	void Start () {
		
	}
	

	private void OnMouseDown()
	{
		Time.timeScale = 0;
		Debug.Log("Door!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
