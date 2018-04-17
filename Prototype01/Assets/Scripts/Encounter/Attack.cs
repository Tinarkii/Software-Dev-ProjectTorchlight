using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour {

	private float timePassed;

	// How many circles have been used in this shape & index for shape[]
	private int touched = 0;

	// The order in which the circles are passed through
	private string[] shape = new string[10];

	// Whether or not the attack phase is done
	private bool finished;

	// An array that stores the intances of combat circle prefab
	GameObject[] circles;

	public int attacksCompleted = 0;

	private float timeAllotted;

	public float time = 3;

	private bool circleConstructed;
	private bool triConstructed;
	private bool boxConstructed;

	// Stuff
	int whichI;
	private bool exitCondition;
	private int enemyHealth;


	public Text E;
	public Text t;


	// Use this for initialization
	void Start () 
	{
		enemyHealth = 100;
		timeAllotted = time;
	}

	// Sets up each of our combat circles
	void BoxConstructor()
	{
		whichI = 4;
		boxConstructed = true;
		triConstructed = false;
		circleConstructed = false;
		// An array that stores the intances of combat circle prefab
		circles = new GameObject[4];
		for (int i = 0; i < 4; i++)
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
			 if (i == 2) circle.transform.position = new Vector3(2, 3, 0);
			 if (i == 3) circle.transform.position = new Vector3(2, 0.5f, 0);
         }
	}

	// Sets up each of our combat circles
	void TriConstructor()
	{
		whichI = 3;
		boxConstructed = false;
		triConstructed = true;
		circleConstructed = false;
		// An array that stores the intances of combat circle prefab
		circles = new GameObject[3];
		for (int i = 0; i < 3; i++)
         {
			 // Instantiates the circle prefab, makes it white, and names it using i
             GameObject circle = (GameObject)Instantiate(Resources.Load("Circle"));
			 circle.GetComponent<Renderer>().material.color = Color.white;
             circle.name = (i+1).ToString();

			 // Stores each instance in the array circles
			 circles[i] = circle;

			 // Positions each circle
			 if (i == 0) circle.transform.position = new Vector3(-2, 0.5f, 0);
			 if (i == 1) circle.transform.position = new Vector3(0, 4, 0);
			 if (i == 2) circle.transform.position = new Vector3(2, 0.5f, 0);
         }
	}


	void CircleConstructor()
	{
		whichI = 6;
		boxConstructed = false;
		triConstructed = false;
		circleConstructed = true;
		// An array that stores the intances of combat circle prefab
		circles = new GameObject[6];
		for (int i = 0; i < 6; i++)
         {
			 // Instantiates the circle prefab, makes it white, and names it using i
             GameObject circle = (GameObject)Instantiate(Resources.Load("Circle"));
			 circle.GetComponent<Renderer>().material.color = Color.white;
             circle.name = (i+1).ToString();

			 // Stores each instance in the array circles
			 circles[i] = circle;

			 // Positions each circle
			 if (i == 0) circle.transform.position = new Vector3(-2, 2, 0);
			 if (i == 1) circle.transform.position = new Vector3(2, 2, 0);
			 if (i == 2) circle.transform.position = new Vector3(1, .25f, 0);
			 if (i == 3) circle.transform.position = new Vector3(-1, .25f, 0);
			 if (i == 4) circle.transform.position = new Vector3(1, 3.75f, 0);
			 if (i == 5) circle.transform.position = new Vector3(-1, 3.75f, 0);
         }
	}


	// Called by EncounterControl to check if Attack phase is over
	public bool Finished() 
	{
		if(finished)
		{
			for (int i = 0; i < whichI; i++)
			{
				// Disables the MouseOverStuff script
				MouseOverStuff.isEnabled = false;

				// Get rid of garbage in the shapes array
				ClearShape();
				Destroy(circles[i]);
			}
		}
		return finished;
	}


	public bool BaddieDefeated()
	{
		return exitCondition;
	}


	void OnEnable() 
	{
		// Sets flags
		finished = false;
		timePassed = 0;
		timeAllotted = time;

		int r = Random.Range(1,4);
		switch (r)
		{
			case 1: 
				BoxConstructor();
				break;
			case 2: 
				TriConstructor();
				break;
			case 3:
				CircleConstructor();
				break;
		}

		// Enable the scripts in circles and turns them white
		for (int i = 0; i < whichI; i++)
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
		if (boxConstructed)
		{
			int pos = System.Array.IndexOf(shape, "1") + System.Array.IndexOf(shape, "2") + System.Array.IndexOf(shape, "3") +
				System.Array.IndexOf(shape, "4");
			if (pos < 6) return false;
			else return true;
		}
		else return false;
	}

	// Checks for an X when called
	private bool CheckCircle()
	{
		if (circleConstructed)
		{
			int pos = System.Array.IndexOf(shape, "1") + System.Array.IndexOf(shape, "2") + System.Array.IndexOf(shape, "3") + System.Array.IndexOf(shape, "4") + 
				System.Array.IndexOf(shape, "5") + System.Array.IndexOf(shape, "6");
			if (pos < 15) 
			{
				return false;
			}
			else return true;
		}
		else return false;
	}

	// Checks for a triangle when called
	private bool CheckTriangle()
	{
		if (triConstructed)
		{
			int pos = System.Array.IndexOf(shape, "1") + System.Array.IndexOf(shape, "2") + System.Array.IndexOf(shape, "3");
			if (pos < 3) 
			{
				return false;
			}
			else return true;
		}
		else return false;
	}

	
	// Update is called once per frame
	void Update () 
	{	
		if (enemyHealth <= 0) exitCondition = true;
		timePassed += Time.deltaTime;

		t.text = "Player's Confidence: " + GameControl.control.Confidence();
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
			enemyHealth -= GameControl.control.DamageToEnemy();
			E.text = "Enemy's Enthusiasm: " + enemyHealth.ToString();
			finished = true;
		}
		else if (CheckCircle()) 
		{
			print("That's a circle.");
			ClearShape();	
			enemyHealth -= GameControl.control.DamageToEnemy();
			E.text = "Enemy's Enthusiasm: " + enemyHealth.ToString();
			finished = true;
		}
		else if (CheckTriangle()) 
		{
			print("That's a triangle.");
			ClearShape();
			enemyHealth -= GameControl.control.DamageToEnemy();
			E.text = "Enemy's Enthusiasm: " + enemyHealth.ToString();
			finished = true;
		}
		finished = (timeAllotted - timePassed) <= 0;
	}
}
