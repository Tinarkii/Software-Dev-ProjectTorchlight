using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An interface for items that can be picked up,
 * put in the inventory, and used by the player
 */

public abstract class Item : MonoBehaviour
{

	/**
	 * How many of this item are in this location
	 * This will make having multiple items in one location easy
	 */
	private int numOfThisItem;

	/**
	 * Use the item
	 */
	abstract public void useItem ();
	
}
