using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOfPlayer : MonoBehaviour {

	/**
	 * @TODO: write this
	 */
    private void OnCollisionEnter (Collision col)
    {
		// The player's shield doesn't care about collisions unless they're with the enemy's blasts
        if (col.gameObject.name != "BlastBad")
			return;

		Debug.Log("SheildOfPlayer has collided with a BlastBad");
    }

}
