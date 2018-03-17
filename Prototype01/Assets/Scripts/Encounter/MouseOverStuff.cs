using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MouseOverStuff : MonoBehaviour {

	// Determines if script is enabled. Controlled by Attack.cs
	public static bool isEnabled;
	public static bool beingTouched;
	public static GameObject thisObject;

	void Start() 
	{
		beingTouched = false;
	}
 	void OnMouseEnter()
	{
		// Ignores the OnMouseEnter function if isEnabled is false
		if (!isEnabled) return;

		// Turns the cirle yellow when the cursor passes over it
		GetComponent<Renderer>().material.color = Color.yellow;

		// Logs which circle was entered into an array and increments the index
		beingTouched = true;
		thisObject = gameObject;
	}
	void OnMouseExit()
	{
		beingTouched = false;
	}
}
