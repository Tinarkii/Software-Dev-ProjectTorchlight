using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public static int touched = 0;
	public static string[] shape = new string[6];

	public static bool finished;

	GameObject[] circles = new GameObject[5];
	GameObject One;
	GameObject Two;
	GameObject Three;
	GameObject Four;
	GameObject Five;

	// Use this for initialization
	void Start () 
	{
		// Initialize and constuct the circles

		One = (GameObject)Instantiate(Resources.Load("Circle"));
		One.transform.position = new Vector3(-2, 0.5f, 0);
		One.GetComponent<Renderer>().material.color = Color.white;
		One.name = "1";
		circles[0] = One;

		Two = (GameObject)Instantiate(Resources.Load("Circle"));
		Two.transform.position = new Vector3(-2, 3, 0);
		Two.GetComponent<Renderer>().material.color = Color.white;
		Two.name = "2";
		circles[1] = Two;			

		Three = (GameObject)Instantiate(Resources.Load("Circle"));
		Three.transform.position = new Vector3(0, 3, 0);
		Three.GetComponent<Renderer>().material.color = Color.white;
		Three.name = "3";
		circles[2] = Three;

		Four = (GameObject)Instantiate(Resources.Load("Circle"));
		Four.transform.position = new Vector3(2, 3, 0);
		Four.GetComponent<Renderer>().material.color = Color.white;
		Four.name = "4";
		circles[3] = Four;

		Five = (GameObject)Instantiate(Resources.Load("Circle"));
		Five.transform.position = new Vector3(2, 0.5f, 0);
		Five.GetComponent<Renderer>().material.color = Color.white;
		Five.name = "5";
		circles[4] = Five;
	}

	public bool Finished() 
	{
		if(finished)
		{
			for (int i = 0; i<=4; i++)
			{
				// Make the circles invisible
				circles[i].GetComponent<Renderer>().material.color = Color.clear;

				// Disable the circle scripts
				circles[i].GetComponent<MouseOverStuff>().enabled = false;

				// Reset shape check
				MouseOverStuff.box = false;
				MouseOverStuff.tri = false;
				MouseOverStuff.x = false;
			}
		}
		return finished;
	}

	void OnEnable() 
	{
		// Unsets finished flag
		finished = false;

		// Enable the scripts in circles
		for (int i = 0; i<=4; i++)
		{
     		circles[i].GetComponent<MouseOverStuff>().enabled = true;
			circles[i].GetComponent<Renderer>().material.color = Color.white;
 		}
	}
	
	// Sets all values in the shape array to null
	void ClearShape()
	{
		for(int i=0; i < 6; i++)
		{
			shape[i] = null;
		}
	}

	// I will create the exit function later...
	public bool ToExit() 
	{
		return false;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		// Check if a shape has been drawn
		if (MouseOverStuff.box) 
		{
			ClearShape();
			Attack.touched = 0;
			print("That's a box.");
			finished = true;
		}
		else if (MouseOverStuff.x) 
		{
			ClearShape();
			Attack.touched = 0;
			print("That's an X.");
			finished = true;
		}
		else if (MouseOverStuff.tri) 
		{
			ClearShape();
			Attack.touched = 0;
			print("That's a triangle.");
			finished = true;
		}
		else if(touched>=6)
		{
			ClearShape();
			touched = 0;
			print("That's a not a shape.");
			for (int i = 0; i<=4; i++)
			{
				circles[i].GetComponent<Renderer>().material.color = Color.white;
			}
		}	
	}
}
