using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class for holding all the player's items
 * @TODO: do we want enemies to have inventories too, so they can give/take items from the player?
 */

public class Inventory : MonoBehaviour
{

	/**
	 * Holds the items
	 */
	private List<Item> items;


	/**
	 * Initialization
	 */
	public void Start () {
		items = new List<Item>();
	}
	
	/**
	 * Add an item to the inventory
	 */
	public void addItem (Item newItem)
	{
		Debug.Log("An Item has been added to the Inventory: " + newItem);

		items.Add (newItem);
		// @TODO: finish implementing this method (make sure there aren't duplicate items in the "items" variable and add appropriate debugging info
	}

}
