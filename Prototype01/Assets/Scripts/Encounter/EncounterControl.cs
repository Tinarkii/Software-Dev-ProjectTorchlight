using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Regulates phase changes and exiting combat in
 * the encounter system
 */
public class EncounterControl : MonoBehaviour {

	[Tooltip("Reference to the enemy that the player is encountering @TODO: there may be a better way to keep track of which enemy is being encountered")]
	public static GameObject enemy;

	// Allows encounters to begin with Defense mode for testing
	// Adjustable from the editor
	public bool DefTestMode;

	// References to various scripts, so they
	// can be enable or disabled for phase changes
	private Defense defScript;
	private DefAction defActScript;
	private Attack attackScript;

	// Reference to the player character
	public GameObject player;

	/* Initialization of scripts, begin in player offense mode. */
	void Start () {
		player = GameObject.Find("Person");
		defScript = Camera.main.GetComponent<Defense>();
		defActScript = player.GetComponent<DefAction>();
		attackScript = Camera.main.GetComponent<Attack>();

		// Should probably instantiate enemy here @TODO: finish this Instantiate(Object original, Vector3 position, Quaternion rotation);
		Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);

		defScript.enabled = false^DefTestMode;
		defActScript.enabled = false^DefTestMode;
		attackScript.enabled = true^DefTestMode;
	}
	
	/* On combat completion, return to previous scene */
	public void ExitCombat() {

		GameControl.control.Load();
	}

	/* This toggles the active offensive and 
	 * defensive scripts when a phase has completed
	 */
	// Update is called once per frame
	void Update () {

		PlayerPrefs.SetInt("Health", GameControl.control.confidence);

		if (attackScript.Finished()) {
			attackScript.enabled = false;
            defScript.enabled = true;
            defActScript.enabled = true;
		}
		if (defScript.Finished()) {
			defScript.enabled = false;
			defActScript.enabled = false;
			attackScript.enabled = true;
		}

		if (attackScript.ToExit() || defScript.ToExit())
			ExitCombat();
	}
}
