using System;
using System.Collections.Generic;
using UnityEngine;

public class Offense : MonoBehaviour {

	// Keep track of whether this script has finished its phase
	private bool finished;


	public bool Finished() {
		return finished;
	}

	// Use this for initialization
	void OnEnable () {
		finished = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Currently this script does nothing but end the phase
		finished = true;
	}
}
