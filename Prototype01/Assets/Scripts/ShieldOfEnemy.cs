using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for the shield made by enemies
 */

public class ShieldOfEnemy : Shield
{
	/**
	 * If a Blast collides with a ShieldOfEnemy, destroy them both
	 */
	protected override void OnCollisionEnter (Collision col)
	{
		// The player's shield doesn't care about collisions unless they're with an enemy's blasts
		if (col.gameObject.name != "Blast")
			return;

		Debug.Log ("ShieldOfEnemy has collided with a Blast");

		Destroy (col.gameObject);
		Destroy (this);
	}

}