using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for the shield made by the player
 */

public class ShieldOfPlayer : MonoBehaviour  
{

	[Tooltip("The time for which this shield will exist")]
	public int shieldTime;

	/**
	 * The time when this object was instantiated
	 */
	private float startTime;

	/**
	 * Start
	 */
	private void Start()
	{
		startTime = Time.time;
	}

	/**
	 * Update
	 */
	private void Update()
	{
		if (Time.time - shieldTime >= startTime)
		{
			Destroy(gameObject);
		}
	}

	/**
	 * If a BlastBad collides with a ShieldOfPlayer, destroy them both
	 */
	protected void OnTriggerEnter (Collider col)
    {
        // The player's shield doesn't care about collisions unless they're with an enemy's blasts
        if (col.gameObject.name != "BlastBad(Clone)")
			return;

		Destroy (col.gameObject);
		Destroy (this);
    }

}