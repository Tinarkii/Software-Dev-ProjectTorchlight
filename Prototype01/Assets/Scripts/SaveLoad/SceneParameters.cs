using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Holds the parameters used in scene changes */
public static class SceneParameters {

	// Defines the scene to return to on combat completion
	public static string retScene = "sample";// This should be set elsewhere

	// Player's health, used both as an input for battle
	// and as the value as a result of battle
	public static int playerHealth = 100;//GameControl.control.confidence;

	// The unique identifier of the enemy entering combat
	public static int enemyID = 0;

}
