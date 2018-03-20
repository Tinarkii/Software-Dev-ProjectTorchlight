using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script for controlling what happens in the TitleScreen scene
 */

public class TitleScreen : MonoBehaviour
{
	/**
	 * Start a new game
	 */
	public void NewGame()
	{
		Debug.Log("NewGame called");
	}

	/**
	 * Load a saved game
	 */
	public void ResumeGame()
	{
		Debug.Log("ResumeGame called");
	}
}
