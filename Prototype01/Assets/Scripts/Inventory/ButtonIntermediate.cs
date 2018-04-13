using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class for a visual inventory button on the player's screen
 */
public class ButtonIntermediate : MonoBehaviour{

    /**
     * When the button is pressed, open up the inventory menu
     */
	public void GoClick(){
		GameControl.control.GetPlayer ().GetComponent<Inventory> ().OpenMenu ();
	}
}
