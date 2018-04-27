using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

/**
 *Script for controlling what happens in the TitleScreen scene
 */
public class TitleScreen : MonoBehaviour
{

	public GameObject loadingScreen;

	void Start()
	{
		loadingScreen.SetActive(false);
	}

	/**
	 *Start a new game
	 */
	public void NewGame()
	{
		loadingScreen.SetActive(true);
		StartCoroutine(WaitNew());
    	
	}

	/**
	*Load a saved game
	*/
    public void ResumeGame()
	{
		loadingScreen.SetActive(true);
		StartCoroutine(WaitResume());
        
	}

	private IEnumerator<WaitForSeconds> WaitNew()
	{
    	yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("Room01");
  	}
	  private IEnumerator<WaitForSeconds> WaitResume()
	{
    	yield return new WaitForSeconds(1.5f);
		GameControl.LoadNew();
  	}
}
