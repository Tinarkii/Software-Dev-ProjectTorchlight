using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * A class to display and control the items menu
 */

public class ItemsMenu : MonoBehaviour
{
	[Tooltip("A prefab for buttons for items in the inventory")]
    public Transform useItemButton;

	/**
	 * Stores a reference to the inventory list so it can be updated as needed
	 */
	private List<Item> items;

	/**
	 * Called when the inventory menu is first opened
	 */
    public void StartInventoryMenu(List<Item> inv)
    {
		items = inv;
		UpdateInventoryMenu ();
    }

	/**
	 * Sets the on-screen menu to match what's in the inventory
	 */
	public void UpdateInventoryMenu()
	{
		// Makes sure all the old buttons are gone before making new ones
		GameObject[] oldButtons = GameObject.FindGameObjectsWithTag("ItemButton");
		foreach (GameObject button in oldButtons)
			Destroy (button);

		// This list may contain null Items, because Items will Destroy themselves when they reach 0 quantity
		items.RemoveAll(Item => Item == null);

		int zPosition = 0;
		foreach (Item i in items)
		{
			// @TODO: For some reason, Items that should have been Destroyed (they have quantity == 0) still get buttons.
			// This is a temporary fix to keep that from happening, but the root cause should be addressed.
			if (i.GetQuantity () <= 0)
				continue;

			Transform t = Instantiate(useItemButton, new Vector3(0, 0, zPosition-=1), Quaternion.identity);
			t.tag = "ItemButton";

			Button button = t.gameObject.GetComponent<Button> ();

			button.GetComponentInChildren<Text>().text = i.Name() + " (" + i.GetQuantity() + ")";

			button.onClick.AddListener(delegate { i.UseItem(); });
			button.onClick.AddListener(delegate { UpdateInventoryMenu(); }); // so that the inventory is kept up-to-date while items are being used

			button.transform.SetParent(this.gameObject.GetComponent<Canvas>().transform,false);
		}
	}

    /**
	 * Resume the game. Should be called by a button being pressed.
	 */
    public void ResumeGame()
    {
        Debug.Log("ResumeGame called");
        Time.timeScale = 1;
        Destroy(gameObject);
    }

}
