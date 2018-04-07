using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Regulates phase changes and exiting combat in
 * the encounter system
 */
public class EncounterControl : MonoBehaviour {

	[Tooltip("Prefab for the enemy that the player is encountering")]
	public static GameObject enemyPrefab;

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

		Instantiate(enemyPrefab, new Vector3(5, 0, 4), Quaternion.Euler(new Vector3(-90,0,-145)));

		defScript.enabled = false^DefTestMode;
		defActScript.enabled = false^DefTestMode;
		attackScript.enabled = true^DefTestMode;
	}

	/* This toggles the active offensive and 
	 * defensive scripts when a phase has completed
	 */
	// Update is called once per frame
	void Update () {

		PlayerPrefs.SetInt("Health", GameControl.control.Confidence());

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

		if (attackScript.BaddieDefeated())
		{	
			GameControl.control.ExitEncounter();
		}
	}
}
