using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	// How many circles have been used in this shape & index for shape[]
	public static int touched = 0;

	// The order in which the circles are passed through
	public static string[] shape = new string[6];

	// Whether or not the attack phase is done
	public static bool finished;

	// An array that stores the intances of combat circle prefab
	GameObject[] circles = new GameObject[5];


	// Use this for initialization
	void Start () 
	{
		CircleConstructor();
	}

	// Sets up each of our combat circles
	void CircleConstructor()
	{
		for (int i = 0; i < 5; i++)
         {
			 // Instantiates the circle prefab, makes it white, and names it using i
             GameObject circle = (GameObject)Instantiate(Resources.Load("Circle"));
			 circle.GetComponent<Renderer>().material.color = Color.white;
             circle.name = (i+1).ToString();

			 // Stores each instance in the array circles
			 circles[i] = circle;

			 // Positions each circle
			 if (i == 0) circle.transform.position = new Vector3(-2, 0.5f, 0);
			 if (i == 1) circle.transform.position = new Vector3(-2, 3, 0);
			 if (i == 2) circle.transform.position = new Vector3(0, 3, 0);
			 if (i == 3) circle.transform.position = new Vector3(2, 3, 0);
			 if (i == 4) circle.transform.position = new Vector3(2, 0.5f, 0);
         }
	}

	// Called by EncounterControl to check if Attack phase is over
	public bool Finished() 
	{
		if(finished)
		{
			Debug.Log("finished was true");
			for (int i = 0; i < 5; i++)
			{
				// Disables the MouseOverStuff script
				MouseOverStuff.isEnabled = false;

				// Make the circles invisible
				circles[i].GetComponent<Renderer>().material.color = Color.clear;
				Debug.Log("Circles were made invisible");

				// Reset shape check
				MouseOverStuff.box = false;
				MouseOverStuff.tri = false;
				MouseOverStuff.x = false;
			}
			Debug.Log("finished is about to be passed to EncounterControl as true");
		}
		return finished;
	}

	void OnEnable() 
	{
		// Unsets finished flag
		finished = false;

		// Enable the scripts in circles and turns them white
		for (int i = 0; i < 5; i++)
		{
			MouseOverStuff.isEnabled = true;
			if(circles[i] != null) circles[i].GetComponent<Renderer>().material.color = Color.white;
 		}
	}
	
	// Sets all values in the shape array to null and resets the index
	void ClearShape()
	{
		for(int i=0; i < 6; i++)
		{
			shape[i] = null;
		}
		touched = 0;
	}

	// I will create the exit function later...
	public bool ToExit() 
	{
		return false;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		// Check if a shape has been drawn, and handles it
		if (MouseOverStuff.box) 
		{
			print("That's a box.");
			ClearShape();
			finished = true;

		}
		else if (MouseOverStuff.x) 
		{
			print("That's an X.");
			ClearShape();		
			finished = true;
		}
		else if (MouseOverStuff.tri) 
		{
			print("That's a triangle.");
			ClearShape();		
			finished = true;
		}
		// If no shape was drawn, reset everything to how it was
		else if(touched>=6)
		{
			print("That's a not a shape.");
			ClearShape();
			for (int i = 0; i<5; i++)
			{
				circles[i].GetComponent<Renderer>().material.color = Color.white;
			}
		}	
	}
}
