using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterControl : MonoBehaviour {

	// References to Defense and Offense scripts,
	// so they can be enable or disabled
	private Defense defScript;
	private Offense offScript;

	// Initialization. Begin in offense mode
	void Start () {
		defScript = Camera.main.GetComponent<Defense>();
		offScript = Camera.main.GetComponent<Offense>();
		defScript.enabled = false;
		offScript.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (defScript.enabled) {
			if (defScript.Finished()) {
				defScript.enabled = false;
				offScript.enabled = true;
			}
		} else if (offScript.enabled) {
			if (offScript.Finished()) {
				offScript.enabled = false;
				defScript.enabled = true;
			}
		}
	}
}
