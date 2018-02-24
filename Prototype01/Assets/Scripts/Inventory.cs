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

	public Transform prefab;


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

		Item listItem = GetItemFromList (newItem);

		if (listItem == null)
		{
			items.Add (newItem);
			Debug.Log("A new Item has been added to the Inventory: " + newItem + ". There is " + newItem.GetQuantity() + " of them in the inventory.");
		}
		else
		{
			listItem.SetQuantity(listItem.GetQuantity () + newItem.GetQuantity ());
			Debug.Log("The number of " + newItem + "s in the Inventory has increased. There are now " + listItem.GetQuantity () + " of them.");
		}

	}

	/**
	 * Get an Item from the items List
	 * @TODO: is there a better way to do this?
	 */
	private Item GetItemFromList (Item newItem)
	{
		foreach (Item listItem in items)
		{
			if (listItem.Name () == newItem.Name ())
				return listItem;
		}

		return null;
	}
		
	/**
	 * Pauses the game and opens the menu. This should
	 * be called by a button or something in the game
	 * @TODO: see also https://gamedev.stackexchange.com/questions/116698/resuming-a-previous-scene-from-a-current-scene-using-the-unity3d
	 * @TODO: Should this method be in another class?
	 */
	public void OpenMenu ()
	{
		Debug.Log ("OpenMenu called");
		Time.timeScale = 0;
		Instantiate (prefab, new Vector3(0, 0, 0), Quaternion.identity);
	}

}
