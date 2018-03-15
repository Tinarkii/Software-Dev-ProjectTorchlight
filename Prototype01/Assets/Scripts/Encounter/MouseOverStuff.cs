using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MouseOverStuff : MonoBehaviour {
	private Color startcolor;
	public static bool box;
	public static bool x;
	public static bool tri;
	private int i;
	// Use this for initialization
	void Start () 
	{
		startcolor = GetComponent<Renderer>().material.color;
	}
	public void AttackEnabled () 
	{
		GetComponent<Renderer>().material.color = Color.white;
	}
 	void OnMouseEnter()
	{
		//startcolor = GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material.color = Color.yellow;	
		Attack.shape[Attack.touched] = this.name;
		Attack.touched = Attack.touched + 1;
		print(string.Join("", Attack.shape));	
		box = string.Join("", Attack.shape).CompareTo("123451")==0;
		x = string.Join("", Attack.shape).CompareTo("14325")==0;
		tri = string.Join("", Attack.shape).CompareTo("1351")==0;
		if (box) 
		{
			for(i=0; i < 6; i++)
			{
				Attack.shape[i] = null;
			}
			Attack.touched = 0;
			print("That's a box.");
		}
		else if (x) 
		{
			for(i=0; i < 6; i++)
			{
				Attack.shape[i] = null;
			}
			Attack.touched = 0;
			print("That's an X.");
		}
		else if (tri) 
		{
			for(i=0; i < 6; i++)
			{
				Attack.shape[i] = null;
			}
			Attack.touched = 0;
			print("That's a triangle.");
		}
		else if(Attack.touched>=6)
		{
			for(i=0; i < 6; i++)
			{
				Attack.shape[i] = null;
			}
			Attack.touched = 0;
			print("That's a not a shape.");
		}	
	}
 	void OnMouseExit()
 	{
  		//GetComponent<Renderer>().material.color = startcolor;
	}
	// Update is called once per frame
	void Update () 
	{
		if (Attack.finished)
		{
			GetComponent<Renderer>().material.color = Color.clear;
		}
	}
}
