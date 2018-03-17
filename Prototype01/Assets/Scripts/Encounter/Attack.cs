using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	private float timePassed;

	// How many circles have been used in this shape & index for shape[]
	private int touched = 0;

	// The order in which the circles are passed through
	private string[] shape = new string[6];

	// Whether or not the attack phase is done
	private bool finished;

	// An array that stores the intances of combat circle prefab
	GameObject[] circles = new GameObject[5];

	public int attacksCompleted = 0;
	public int roundsPassed = 0;

	public float timeAllotted;


	// Use this for initialization
	void Start () 
	{
		timeAllotted = 3;
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
			for (int i = 0; i < 5; i++)
			{
				// Disables the MouseOverStuff script
				MouseOverStuff.isEnabled = false;

				// Make the circles invisible
				circles[i].GetComponent<Renderer>().material.color = Color.clear;

				// Get rid of garbage in the shapes array
				ClearShape();
			}
		}
		return finished;
	}

	public bool ToExit()
	{
		if(attacksCompleted >= 4)
		{
			print("You have beaten the opponent");
			return true;
		}

		if(roundsPassed >= 8)
		{
			print("You lost to the opponent");
			return true;
		}
		else return false;
	}



	void OnEnable() 
	{
		// Sets flags
		finished = false;
		timePassed = 0;
		timeAllotted = 3;
		roundsPassed++;

		// Enable the scripts in circles and turns them white
		for (int i = 0; i < 5; i++)
		{
			MouseOverStuff.isEnabled = true;
			if(circles[i] != null) circles[i].GetComponent<Renderer>().material.color = Color.white;
 		}
	}


	// Handles a circle being touched by the cursor
	public void CircleTouched(GameObject x)
	{
		// Logs which circle was entered into an array and increments the index
		shape[touched] = x.name;
		touched++;

		Debug.Log(string.Join("", shape));
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



	// Method to prevent accdentally hitting a circle more than once
	void NoRepeats()
	{
		// Prevents any null pointer exceptions
		if (touched > 1 && shape != null)
		{
			// Checks last two entries to shape array
			if (shape[touched-1].CompareTo(shape[touched - 2]) == 0)
			{
				// Removes the most recent and decrements the index
				shape[touched - 1] = null;
				touched--;
			}
		}
		// If no repeats, do nothing
		else return;
	}



	// Checks for a box when called
	private bool CheckBox()
	{
		return string.Join("", shape).CompareTo("123451") == 0 ||
			string.Join("", shape).CompareTo("154321") == 0 ||
			string.Join("", shape).CompareTo("234512") == 0 || 
			string.Join("", shape).CompareTo("215432") == 0 || 
			string.Join("", shape).CompareTo("345123") == 0 || 
			string.Join("", shape).CompareTo("321543") == 0 || 
			string.Join("", shape).CompareTo("451234") == 0 ||
			string.Join("", shape).CompareTo("432154") == 0 || 
			string.Join("", shape).CompareTo("512345") == 0 ||
			string.Join("", shape).CompareTo("543215") == 0;
	}

	// Checks for an X when called
	private bool CheckX()
	{
		return string.Join("", shape).CompareTo("14325") == 0 ||
			string.Join("", shape).CompareTo("52341") == 0;
	}

	// Checks for a triangle when called
	private bool CheckTriangle()
	{
		return string.Join("", shape).CompareTo("1351") == 0 ||			
			string.Join("", shape).CompareTo("1531") == 0 ||
			string.Join("", shape).CompareTo("3513") == 0 ||
			string.Join("", shape).CompareTo("3153") == 0 ||
			string.Join("", shape).CompareTo("5135") == 0 ||
			string.Join("", shape).CompareTo("5315") == 0;
	}

	// Catches a wrong shape earlier
	private bool WrongShape()
	{
		return string.Join("", shape).CompareTo("312") == 0 ||
			string.Join("", shape).CompareTo("314") == 0 ||
			string.Join("", shape).CompareTo("323") == 0 || 
			string.Join("", shape).CompareTo("324") == 0 || 
			string.Join("", shape).CompareTo("325") == 0 || 
			string.Join("", shape).CompareTo("341") == 0 || 
			string.Join("", shape).CompareTo("342") == 0 ||
			string.Join("", shape).CompareTo("343") == 0 || 
			string.Join("", shape).CompareTo("352") == 0 ||
			string.Join("", shape).CompareTo("353") == 0 ||
			string.Join("", shape).CompareTo("354") == 0 || 
			string.Join("", shape).CompareTo("121") == 0 || 
			string.Join("", shape).CompareTo("124") == 0 || 
			string.Join("", shape).CompareTo("125") == 0 ||
			string.Join("", shape).CompareTo("131") == 0 || 
			string.Join("", shape).CompareTo("132") == 0 ||
			string.Join("", shape).CompareTo("134") == 0 ||
			string.Join("", shape).CompareTo("151") == 0 || 
			string.Join("", shape).CompareTo("152") == 0 || 
			string.Join("", shape).CompareTo("24") == 0 || 
			string.Join("", shape).CompareTo("25") == 0 || 
			string.Join("", shape).CompareTo("212") == 0 ||
			string.Join("", shape).CompareTo("213") == 0 || 
			string.Join("", shape).CompareTo("214") == 0 ||
			string.Join("", shape).CompareTo("231") == 0 ||
			string.Join("", shape).CompareTo("232") == 0 || 
			string.Join("", shape).CompareTo("235") == 0 || 
			string.Join("", shape).CompareTo("41") == 0 || 
			string.Join("", shape).CompareTo("42") == 0 || 
			string.Join("", shape).CompareTo("431") == 0 ||
			string.Join("", shape).CompareTo("434") == 0 || 
			string.Join("", shape).CompareTo("435") == 0 ||
			string.Join("", shape).CompareTo("452") == 0 ||
			string.Join("", shape).CompareTo("453") == 0 ||
			string.Join("", shape).CompareTo("454") == 0 ||
			string.Join("", shape).CompareTo("514") == 0 ||
			string.Join("", shape).CompareTo("515") == 0 ||
			string.Join("", shape).CompareTo("521") == 0 ||
			string.Join("", shape).CompareTo("523") == 0 ||
			string.Join("", shape).CompareTo("524") == 0 ||
			string.Join("", shape).CompareTo("525") == 0 ||
			string.Join("", shape).CompareTo("532") == 0 ||
			string.Join("", shape).CompareTo("534") == 0 ||
			string.Join("", shape).CompareTo("535") == 0 ||
			string.Join("", shape).CompareTo("541") == 0 ||
			string.Join("", shape).CompareTo("542") == 0 ||
			string.Join("", shape).CompareTo("545") == 0;
	}

	
	// Update is called once per frame
	void Update () 
	{	
		timePassed += Time.deltaTime;

		// Prevents accidentally double hitting a circle
		NoRepeats();

		// Handles a circle being touched
		if (MouseOverStuff.beingTouched) 
		{
			CircleTouched(MouseOverStuff.thisObject);
			MouseOverStuff.beingTouched = false;
		}

		// Check if a shape has been drawn, and handles it
		if (CheckBox()) 
		{
			print("That's a box.");
			ClearShape();
			attacksCompleted++;
			finished = true;
		}
		else if (CheckX()) 
		{
			print("That's an X.");
			ClearShape();	
			attacksCompleted++;	
			finished = true;
		}
		else if (CheckTriangle()) 
		{
			print("That's a triangle.");
			ClearShape();
			attacksCompleted++;		
			finished = true;
		}
		else if (WrongShape())
		{
			print("That's a not a shape.");
			ClearShape();
			for (int i = 0; i<5; i++)
			{
				circles[i].GetComponent<Renderer>().material.color = Color.white;
			}
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
		finished = (timeAllotted - timePassed) <= 0;
	}
}
