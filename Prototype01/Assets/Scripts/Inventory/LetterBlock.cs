using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for letter blocks, which give the player's defense when used
 */

public class LetterBlock : DefenceItem
{
	/**
	 * Initialization: set the name and defense increase
	 */
	public void Start()
	{
		myName = "Fruit Snacks";
		defenseIncrease = 30;
	}
}