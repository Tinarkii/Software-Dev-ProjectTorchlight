using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Script for controlling what happens in the TitleScreen scene
 */

public class TitleScreen : MonoBehaviour
{
	[Tooltip("A prefab for the GameControl object")]
	public GameObject gameControlPrefab;

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
		gameControlPrefab.GetComponent<GameControl>().Load();
		////@TODO: This works, but it seems kinda sloppy. Is there a better way of doing this?
	}
}
