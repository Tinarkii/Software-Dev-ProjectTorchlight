using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for the shield made by enemies
 */

public class ShieldOfEnemy : EncounterElement
{
    private int time = 0;

	private void Update() {
		// A very hackish way to do move the object.
		// If someone would be so kind as to provide a less hackish solution,
		// it would be much appreciated.
		transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        time++;
        if(time >= 250)
        {
            Destroy(transform.gameObject);
        }
	}

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