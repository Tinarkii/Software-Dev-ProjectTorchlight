using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for items that restore confidence
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
	 * Use the ConfidenceItem - increases the player's confidence
	 */
	protected override void UseAction ()
	{
		Debug.Log ("A " + myName + " was used. The player's confidence meter should be increased by " + healthRegained + ".");
		GameControl.control.AdjustConfidenceBy(healthRegained);
	}

}