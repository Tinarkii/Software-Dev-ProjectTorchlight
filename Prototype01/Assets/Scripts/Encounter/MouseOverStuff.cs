using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MouseOverStuff : MonoBehaviour {

	// Determines if script is enabled. Controlled by Attack.cs
	public static bool isEnabled;
	public static bool beingTouched;
	public static GameObject thisObject;
	public bool beenTouched;

	void Start() 
	{
		beingTouched = false;
	}
	
	public bool GetBeenTouched()
	{
		return beenTouched;
	}

 	void OnMouseEnter()
	{
		// Ignores the OnMouseEnter function if isEnabled is false
		if (!isEnabled) return;

		// Turns the cirle yellow when the cursor passes over it
		GetComponent<Renderer>().material.color = Color.yellow;

		// Tells Attack.cs that circle was touched, and which circle this is
		beingTouched = true;
		beenTouched = true;
		thisObject = gameObject;
	}

	void OnMouseExit()
	{
		beingTouched = false;
	}
}
