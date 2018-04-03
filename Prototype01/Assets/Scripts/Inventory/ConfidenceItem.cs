using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A sample Item for testing purposes
 */

public class ConfidenceItem : Item
{
	[Tooltip("The name of this Item")]
	public string itemsName;

	[Tooltip("How much confidence is regained when this Item is used")]
	public int healthRegained;

	/**
	 * Initialization
	 */
	public void Start ()
	{
		myName = itemsName;
	}

	/**
	 * Use the SampleItem - creates another SampleItem on the screen
	 */
	public override void UseAction ()
	{
		Debug.Log ("A " + myName + " was used. The player's confidence meter should be increased by " + healthRegained + ".");
	}

}