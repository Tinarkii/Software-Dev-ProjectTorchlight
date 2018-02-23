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
		if (newItem == null)
		{
			Debug.LogWarning("There was an attempt to add null to Inventory");
			return;
		}

		if (items.Contains (newItem)) //@TODO: Will this work as expected?
		{
			//@TODO: add the item
			Debug.Log("The number of " + newItem + "s in the Inventory has increaded. There are now " + "@TODO: finish this" + " of them.");		
		}
		else
		{
			items.Add (newItem);
			Debug.Log("A new Item has been added to the Inventory: " + newItem + ". There are " + newItem.Quantity() + " of them in the inventory.");
		}

	}

	//@TODO: there should be a way to see and use items

}
