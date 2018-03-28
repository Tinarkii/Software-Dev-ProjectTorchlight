using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class for objects that bob up and down
 */

public class Bob : MonoBehaviour
{
	[Tooltip("The object that bobs up and down")]
	public Rigidbody rigidBody;

	[Tooltip("How much the object bobs")]
	public float magnitude;

	[Tooltip("The rate at which the object bobs")]
	public float rate;

	/**
	 * Called once per frame. FixedUpdated is used rather than Update to avoid interference with the in-game physics.
	 */
	void FixedUpdate ()
	{
		rigidBody.velocity = new Vector3 (rigidBody.velocity.x, magnitude*Mathf.Sin(Time.time*rate), rigidBody.velocity.z);
	}
}
