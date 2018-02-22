using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An interface for items that can be picked up,
 * put in the inventory, and used by the player
 */

public interface Item {

	/**
	 * Use the item
	 */
	void useItem();
	
}
