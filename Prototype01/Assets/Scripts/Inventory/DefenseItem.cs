using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A superclass for items that increase defense
 */

public abstract class DefenceItem : Item
{
	/**
	 * How much defense is increased by when this Item is used - set in subclasses
	 */
	protected int defenseIncrease;

	/**
	 * Use the DefenceItem - increases the player's defense
	 */
	protected override void UseAction ()
	{
		Debug.Log ("A " + myName + " was used. The player's defense should be increased by " + defenseIncrease + ".");
		Debug.LogError ("Method not yet implemented");
	}

}