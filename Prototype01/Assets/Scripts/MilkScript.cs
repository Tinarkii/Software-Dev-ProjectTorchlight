using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkScript : MonoBehaviour {

	/* When the player reaches this object, load the final scene */
	protected void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Player")
			GameControl.LoadScene("EndScene");
	}
}
