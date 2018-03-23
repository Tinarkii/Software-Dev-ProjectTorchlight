using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class for objects that bob up and down
 */

public class Bob : MonoBehaviour
{
	public Rigidbody rigidBody;
	public float magnitude;
	public float rate;

	// Update is called once per frame
	void Update ()
	{
		rigidBody.velocity = new Vector3 (rigidBody.velocity.x, magnitude*Mathf.Sin(Time.time*rate), rigidBody.velocity.z);
	}
}
