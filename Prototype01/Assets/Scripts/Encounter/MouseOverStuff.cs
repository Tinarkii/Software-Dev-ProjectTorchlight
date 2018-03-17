using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MouseOverStuff : MonoBehaviour {
	public static bool box;
	public static bool x;
	public static bool tri;
	private Color startcolor;
	private int i;
	void Start () 
	{
	}


 	void OnMouseEnter()
	{

		GetComponent<Renderer>().material.color = Color.yellow;	
		Attack.shape[Attack.touched] = this.name;
		Attack.touched = Attack.touched + 1;
		print(string.Join("", Attack.shape));	
		box = string.Join("", Attack.shape).CompareTo("123451")==0;
		x = string.Join("", Attack.shape).CompareTo("14325")==0;
		tri = string.Join("", Attack.shape).CompareTo("1351")==0;
	}



	void OnEnable() 
	{
	}

 	void OnMouseExit()
 	{	
	}

	// Update is called once per frame
	void Update () 
	{
	}
}
