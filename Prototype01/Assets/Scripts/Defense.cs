using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	// Keep track of whether this script has finished its phase
	private bool finished;
	
	/* Keeps track of whether the defensive phase has ended
	 */
	public bool Finished() {
		return finished;
	}

	// Use this for initialization
	void OnEnable () {
		finished = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
