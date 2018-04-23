using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Returns from end scene to main menu
 */
public class EndScene : MonoBehaviour {
	/* Go to menu */
	public void Menu() {
		SceneManager.LoadScene("TitleScreen");
	}
}
