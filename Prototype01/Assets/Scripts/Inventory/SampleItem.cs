using System.Collections;
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
	 * Creates a SampleItem near position (0,0,0)
	 */
	protected override void UseAction ()
	{
		Instantiate ((GameObject)Resources.Load("SampleItemResource", typeof(GameObject)), new Vector3 (Random.Range(-15,15), 1, Random.Range(-15,15)), Quaternion.identity);
	}
	
}
