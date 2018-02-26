using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterControl : MonoBehaviour {

	// References to various scripts, so they
	// can be enable or disabled for phase changes
	private Defense defScript;
	private DefAction defActScript;
	private Offense offScript;

	// Reference to the player character
	public GameObject player;

	// Initialization. Begin in player offense mode
	void Start () {
		defScript = Camera.main.GetComponent<Defense>();
		defActScript = player.GetComponent<DefAction>();
		offScript = Camera.main.GetComponent<Offense>();
		defScript.enabled = false;
		defActScript.enabled = false;
		offScript.enabled = true;
	}
	
	/* This toggles the active offensive and 
	 * defensive scripts when a phase has completed
	 */
	// Update is called once per frame
	void Update () {
		if (defScript.Finished()) {
			defScript.enabled = false;
			defActScript.enabled = false;
			offScript.enabled = true;
		}
		if (offScript.Finished()) {
			offScript.enabled = false;
			defScript.enabled = true;
			defActScript.enabled = true;
		}
	}
}
