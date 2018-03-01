using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for the shield made by the player
 */

public class ShieldOfPlayer : Shield
{
	/**
	 * If a BlastBad collides with a ShieldOfPlayer, destroy them both
	 */
	protected override void OnCollisionEnter (Collision col)
    {
		// The player's shield doesn't care about collisions unless they're with an enemy's blasts
        if (col.gameObject.name != "BlastBad")
			return;

		Debug.Log ("ShieldOfPlayer has collided with a BlastBad");

		Destroy (col.gameObject);
		Destroy (this);
    }

}