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
	 * Use the ConfidenceItem - restores confidence
	 */
	public override void UseAction ()
	{
		Debug.LogWarning ("The function UseAction in ConfidenceItem.cs has not been implemented.");
		//Debug.Log ("A " + myName + " was used. The player's confidence meter should be increased by " + healthRegained + ".");
	}

}