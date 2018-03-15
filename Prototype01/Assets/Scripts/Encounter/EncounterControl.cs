using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterControl : MonoBehaviour {

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

	// Initialization. Begin in player offense mode
	void Start () {
		defScript = Camera.main.GetComponent<Defense>();
		defActScript = player.GetComponent<DefAction>();
		attackScript = Camera.main.GetComponent<Attack>();
		defScript.enabled = false^DefTestMode;
		defActScript.enabled = false^DefTestMode;
		attackScript.enabled = true^DefTestMode;
	}
	
	public void ExitCombat()
	{
		SceneManager.LoadScene("sample");
	}

	/* This toggles the active offensive and 
	 * defensive scripts when a phase has completed
	 */
	// Update is called once per frame
	void Update () {
		if (attackScript.Finished()) 
		{
			if (attackScript.ToExit())
                ExitCombat();
			attackScript.enabled = false;
            defScript.enabled = true;
            defActScript.enabled = true;
		}
		/*if (defScript.Finished()) 
		{
			if (defScript.ToExit())
				ExitCombat();
			defScript.enabled = false;
			defActScript.enabled = false;
			attackScript.enabled = true;
		}*/

	}
}
