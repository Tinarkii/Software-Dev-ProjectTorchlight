using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class for holding all the player's items
 */

public class Inventory : MonoBehaviour
{
    [Tooltip("A prefab for the items menu that will be instantiated when the inventory button is pressed")]
    public Transform itemsMenuPrefab;

    /**
	 * Holds the items
	 */
    private List<Item> items;


    /**
	 * Initialization
	 */
    public void Start()
    {
        items = new List<Item>();
    }

    /**
	 * Add an item to the inventory
	 */
    public void addItem(Item newItem)
    {
        if (newItem == null)
        {
            Debug.LogWarning("There was an attempt to add null to Inventory");
            return;
        }

        Item listItem = GetItemFromList(newItem);

        if (listItem == null)
        {
            items.Add(newItem);
            Debug.Log("A new Item has been added to the Inventory: " + newItem + ". There is " + newItem.GetQuantity() + " of them in the inventory.");
        }
        else
        {
            listItem.SetQuantity(listItem.GetQuantity() + newItem.GetQuantity());
            Debug.Log("The number of " + newItem + "s in the Inventory has increased. There are now " + listItem.GetQuantity() + " of them.");
        }

    }

    /**
	 * Get an Item from the items List
	 */
    private Item GetItemFromList(Item newItem)
    {
		// This list may contain null Items, because Items will Destroy themselves when they reach 0 quantity
		items.RemoveAll(Item => Item == null);

        foreach (Item listItem in items)
        {

            if (listItem.Name() == newItem.Name())
                return listItem;
        }

        return null;
    }

    /**
	 * Pauses the game and opens the menu. This should
	 * be called by a button or something in the game
	 */
    public void OpenMenu()
    {
		Debug.Log("OpenMenu called. There are " + items.Count + " item types in the inventory.");
        Time.timeScale = 0;
        Transform t = Instantiate(itemsMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		t.gameObject.GetComponent<ItemsMenu> ().StartInventoryMenu (items);
    }

}
