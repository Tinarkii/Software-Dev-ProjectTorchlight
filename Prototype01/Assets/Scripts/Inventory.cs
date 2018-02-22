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
	public List<Item> items;


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
		//@TODO: needs to be implemened
	}

}
