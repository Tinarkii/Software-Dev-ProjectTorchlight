using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A superclass for items that increase attack strength
 */

public abstract class AttackItem : Item
{
	/**
	 * How much attack is increased by when this Item is used - set in subclasses
	 */
	protected int attackIncrease;

	/**
	 * Use the AttackItem - increases the player's attack
	 */
	protected override void UseAction ()
	{
		Debug.Log ("A " + myName + " was used. The player's offensive strength should be increased by " +attackIncrease + ".");
		GameControl.control.AdjustDamageToEnemyBy (attackIncrease);
	}

}