using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MouseOverStuff : MonoBehaviour {

	// boolean values for checking if a shape was drawn
	public static bool box;
	public static bool x;
	public static bool tri;

	// Determines if script is enabled. Controlled by Attack.cs
	public static bool isEnabled;

 	void OnMouseEnter()
	{
		// Ignores the OnMouseEnter function if isEnabled is false
		if (!isEnabled) return;

		// Turns the cirle yellow when the cursor passes over it
		GetComponent<Renderer>().material.color = Color.yellow;	

		// Logs which circle was entered into an array and increments the index
		Attack.shape[Attack.touched] = this.name;
		Attack.touched = Attack.touched + 1;

		print(string.Join("", Attack.shape));	

		// Checks the above array for any recognized shapes and toggles any relevant boolean
		box = string.Join("", Attack.shape).CompareTo("123451") == 0 ||
			string.Join("", Attack.shape).CompareTo("234512") == 0 || 
			string.Join("", Attack.shape).CompareTo("345123") == 0 || 
			string.Join("", Attack.shape).CompareTo("451234") == 0 || 
			string.Join("", Attack.shape).CompareTo("512345") == 0;

		x = string.Join("", Attack.shape).CompareTo("14325") == 0 ||
			string.Join("", Attack.shape).CompareTo("52341") == 0;

		tri = string.Join("", Attack.shape).CompareTo("1351") == 0 ||			
			string.Join("", Attack.shape).CompareTo("1531") == 0 ||
			string.Join("", Attack.shape).CompareTo("3513") == 0 ||
			string.Join("", Attack.shape).CompareTo("3153") == 0 ||
			string.Join("", Attack.shape).CompareTo("5135") == 0 ||
			string.Join("", Attack.shape).CompareTo("5315") == 0;
	}
}
