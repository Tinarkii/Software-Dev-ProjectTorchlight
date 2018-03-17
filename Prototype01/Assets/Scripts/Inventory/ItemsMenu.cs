﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * A class to display and control the items menu
 */

public class ItemsMenu : MonoBehaviour
{

    public Transform useItemButton;

    // public Transform textBox;

    private Inventory inventory;

	// Should run when the ItemsMenuCanvas is instantiated
	public void Awake ()
	{
		Debug.Log ("Awake called in ItemsMenu script");

		Button button = GameObject.Find ("ResumeButton").GetComponent<Button>();
		button.GetComponentInChildren<Text>().text = "text changed";
		button.onClick.AddListener(delegate { ResumeGame(); });
	}

    public void UpdateInventory(List<Item> inv)
    {
        Debug.Log(inv[0]);
        //  inventory = inv;
        Debug.Log("Start Method Begins");
        List<Item> itemList = inv;
        Debug.Log(itemList[0]);

        Instantiate(useItemButton, new Vector3(0, 0, 0), Quaternion.identity);

        int j = 0;
        foreach (Item i in itemList)
        {
            Debug.Log(i);
            Debug.Log("foreach");
            Instantiate(useItemButton, new Vector3(0, 0, 0), Quaternion.identity);
            j++;
        }

        ////Debug.Log(itemList[0]);
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
