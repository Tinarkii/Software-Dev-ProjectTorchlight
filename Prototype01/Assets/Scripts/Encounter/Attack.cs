using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public static int touched = 0;
	public static string[] shape = new string[6];

	public static bool finished;

	// Use this for initialization
	void Start () {}

	public bool Finished() 
	{
		return finished;
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
	}
}
