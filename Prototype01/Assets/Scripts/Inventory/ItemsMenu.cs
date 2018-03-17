using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * A class to display and control the items menu
 */

public class ItemsMenu : MonoBehaviour {

	// Should run when the ItemsMenuCanvas is instantiated
	public void Awake ()
	{
		Debug.Log ("Awake called in ItemsMenu script");

		Button button = GameObject.Find ("ResumeButton").GetComponent<Button>();
		button.GetComponentInChildren<Text>().text = "text changed";
		button.onClick.AddListener(delegate { ResumeGame(); });
	}

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
