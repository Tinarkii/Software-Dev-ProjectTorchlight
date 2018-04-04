using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIntermediate : MonoBehaviour{

	public void GoClick(){
		GameControl.control.GetPlayer ().GetComponent<Inventory> ().OpenMenu ();
	}
}
