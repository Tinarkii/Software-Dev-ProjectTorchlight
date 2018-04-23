using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for fruit snacks, which give the player confidence when used
 */

public class FruitSnacks : ConfidenceItem
{
	/**
	 * Initialization: set the name and confidence regain
	 */
	public void Start()
	{
		myName = "Fruit Snacks";
		healthRegained = 30;
		description = "adds " + healthRegained + " to cofidence";
	}
}