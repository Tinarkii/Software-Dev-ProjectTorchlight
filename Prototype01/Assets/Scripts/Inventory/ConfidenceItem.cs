using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A superclass for items that restore confidence
 */

public abstract class ConfidenceItem : Item
{
	/**
	 * How much confidence is regained when this Item is used - set in subclasses
	 */
	protected int healthRegained;

	/**
	 * Use the ConfidenceItem - increases the player's confidence
	 */
	protected override void UseAction ()
	{
		Debug.Log ("A " + myName + " was used. The player's confidence meter should be increased by " + healthRegained + ".");
		GameControl.control.AdjustConfidenceBy(healthRegained);
	}

}