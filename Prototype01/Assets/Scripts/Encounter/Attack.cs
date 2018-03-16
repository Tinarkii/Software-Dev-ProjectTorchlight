using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public static int touched = 0;
	public static string[] shape = new string[6];

	public static bool finished;
	public static bool attackEnabled;

	// Use this for initialization
	void Start () 
	{
	}

	public bool Finished() 
	{
		return finished;
	}

	void OnEnable() 
	{
		attackEnabled = true;
		finished = false;
		GameObject[] GOs = GameObject.FindGameObjectsWithTag("Circles");
		for (int i = 0; i<GOs.Length; i++)
		{
     		GOs[i].GetComponent<MouseOverStuff>().enabled = true;
 		}
	}
	
	public bool ToExit() 
	{
		return false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (MouseOverStuff.box || MouseOverStuff.x || MouseOverStuff.tri)
			finished = true;
		if (finished)
			attackEnabled = false;
	}
}
