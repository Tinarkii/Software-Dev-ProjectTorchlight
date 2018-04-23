using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		SceneManager.LoadScene("Room01");
	}

	/**
	 * Load a saved game
	 */
	public void ResumeGame()
	{
		GameControl.LoadNew();
	}
}
