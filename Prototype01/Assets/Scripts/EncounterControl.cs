using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterControl : MonoBehaviour {

	// References to Defense and Offense scripts,
	// so they can be enable or disabled
	private Defense defScript;
	private Offense offScript;

	// Initialization. Begin in player offense mode
	void Start () {
		defScript = Camera.main.GetComponent<Defense>();
		offScript = Camera.main.GetComponent<Offense>();
		defScript.enabled = false;
		offScript.enabled = true;
	}
	
	/* This toggles the active offensive and 
	 * defensive scripts when a phase has completed
	 */
	// Update is called once per frame
	void Update () {
		if (defScript.Finished()) {
			defScript.enabled = false;
			offScript.enabled = true;
		}
		if (offScript.Finished()) {
			offScript.enabled = false;
			defScript.enabled = true;
		}
	}
}
