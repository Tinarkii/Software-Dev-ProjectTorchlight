using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterControl : MonoBehaviour {

	// References to various scripts, so they
	// can be enable or disabled for phase changes
	private Defense defScript;
	private DefAction defActScript;
	private TestforCombat TestforCombat;

	// Reference to the player character
	public GameObject player;

	// Initialization. Begin in player offense mode
	void Start () {
		defScript = Camera.main.GetComponent<Defense>();
		defActScript = player.GetComponent<DefAction>();
		TestforCombat = Camera.main.GetComponent<TestforCombat>();
		defScript.enabled = false;
		defActScript.enabled = false;
		TestforCombat.enabled = true;
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
		if (defScript.Finished()) {
			if (defScript.ToExit())
				ExitCombat();
			defScript.enabled = false;
			defActScript.enabled = false;
			TestforCombat.enabled = true;
		}
		if (TestforCombat.Finished()) {
			if (TestforCombat.ToExit())
                ExitCombat();
			TestforCombat.enabled = false;
            defScript.enabled = true;
            defActScript.enabled = true;
		}
	}
}
