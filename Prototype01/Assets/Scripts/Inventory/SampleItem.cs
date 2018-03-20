﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A sample Item for testing purposes
 */

public class SampleItem : Item
{
	/**
	 * Initialization
	 */
	public void Start ()
	{
		myName = "SampleItem";
	}

	/**
	 * Use the SampleItem - creates another SampleItem on the screen
	 */
	public override void UseItem ()
	{
		Debug.Log("SampleItem used");

		Instantiate ((GameObject)Resources.Load("SampleItemResource", typeof(GameObject)), new Vector3 (Random.Range(-15,15), 1, Random.Range(-15,15)), Quaternion.identity);

		quantity--;

		if (quantity <= 0)
			Destroy (this);
	}
	
}
