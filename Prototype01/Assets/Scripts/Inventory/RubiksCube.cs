using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script for Rubik's Cubes, which give the player's offensive strength when used
 */

public class RubiksCube : AttackItem
{
	/**
	 * Initialization: set the name and attack strength increase
	 */
	public void Start()
	{
		myName = "Rubik's Cube";
		attackIncrease = 30;
	}
}