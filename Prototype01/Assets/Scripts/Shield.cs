using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An abstract script for all shields, whether the player's or enemys'
 * @TODO: Is it worth making ShieldOfPlayer and ShieldOfEnemy just instances of this class?
 */

public abstract class Shield : MonoBehaviour
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
			Debug.Log ("A Shield has reached the end of its existence time and will now be destroyed");
			Destroy(gameObject);
		}
	}

	/**
	 * Every Shield will have to handle collisions somehow
	 */
	protected abstract void OnCollisionEnter(Collision col);

}
