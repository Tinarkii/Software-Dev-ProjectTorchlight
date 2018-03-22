using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for the shield made by enemies
 */

public class ShieldOfEnemy : MonoBehaviour
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
	protected void OnTriggerEnter (Collider col) {
		// The enemy's shield doesn't care about collisions unless they're with a player's blast
		if (col.gameObject.name != "Blast(Clone)")
			return;

		Destroy (col.gameObject);
		Destroy (transform.gameObject);
	}
	
}