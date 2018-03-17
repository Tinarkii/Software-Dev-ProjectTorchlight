using System.Collections;
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

    public void UpdateInventory(List<Item> inv)
    {
        //Debug.Log(inv[0]);
        //  inventory = inv;
        Debug.Log("Start Method Begins");
        //List<Item> itemList = inv;
        //Debug.Log(itemList[0]);

        //Instantiate(useItemButton, new Vector3(0, 0, 0), Quaternion.identity);

        //int j = 0;
        foreach (Item i in inv)
        {
            //Debug.Log(i);
            //Debug.Log("foreach");
			Transform t = Instantiate(useItemButton, new Vector3(0, 0, 0), Quaternion.identity); ////WARNING: not working yet
			Button button = t.gameObject.GetComponent<Button> ();
			button.GetComponentInChildren<Text>().text = i.Name();
			button.onClick.AddListener(delegate { i.UseItem(); });
            //j++;
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
