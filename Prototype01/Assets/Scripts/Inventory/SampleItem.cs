using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A sample Item for testing purposes
 */

public class SampleItem : Item
{
	[Tooltip("A prefab so SampleItem can make more of itself when used")]
	public Transform SampleItemPrefab;

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

		//@TODO: WHY DOESN'T THIS WORK WHAT DO YOU WANT FROM ME
		Instantiate (SampleItemPrefab, new Vector3 (0, 1, 0), Quaternion.identity);

		if (quantity > 1)
			quantity--;
		else
			Destroy (this);
	}
	
}
