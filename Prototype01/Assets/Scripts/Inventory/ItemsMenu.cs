using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class to display and control the items menu
 */

public class ItemsMenu : MonoBehaviour {

	/**
	 * Resume the game. Should be called by a button being pressed.
	 */
	public void ResumeGame ()
	{
		Debug.Log ("ResumeGame called");
		Time.timeScale = 1;
		Destroy (gameObject);
	}

}
