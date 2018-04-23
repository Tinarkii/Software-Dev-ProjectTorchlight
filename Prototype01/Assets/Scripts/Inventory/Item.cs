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
	 * How many of this item are in this location (all items start with a quantity of 1)
	 */
	private int quantity = 1;
	private int index = -1; //position in array of item

	public bool picked = false; //if the item has been picked up
	private GameObject sceneControl;

	/**
	 * The name of this kind of Item
	 */
	protected string myName;

	/**
	 * Returns the name of this Item
	 */
	public string Name ()
	{
		return myName;
	}

	/**
	 * Returns the number of items of this type that are here
	 */
	public int GetQuantity ()
	{
		return quantity;
	}

	/**
	 * Returns the number of items of this type that are here
	 */
	public void SetQuantity (int newQuantity)
	{
		quantity = newQuantity;
	}

	/**
	 * If an Item object is lying around and the player
	 * collides with it, remove it from the overworld and
	 * add it to the inventory
	 */
    private void OnCollisionEnter (Collision col)
    {
		// Items don't care about collisions unless they're with the player
		if (col.gameObject.name != "Person" && col.gameObject.name != "Player")
			return;

		Debug.Log("Person has collided with a " + this);

		Inventory personsInventory = (Inventory)col.gameObject.GetComponent (typeof(Inventory));
		
		if (personsInventory == null) {
			Debug.LogError ("Unable to access Inventory");
			return;
		}

		personsInventory.addItem (this);
		sceneControl.GetComponent<SceneControl> ().UpdateItem (index);
		picked = true;
    }

	/**
	 * Use this Item
	 */
	public void UseItem ()
	{
		Debug.Log("Item (" + myName + ") used");

		UseAction ();

		quantity--;

		if (quantity <= 0)
			Destroy (this);
	}

	void Awake(){
		sceneControl = GameObject.Find ("SceneControl");

		if (sceneControl == null)
			Debug.LogError ("Cannot find SceneControl");
	}

	void FixedUpdate(){
		if (picked) {
			gameObject.SetActive (false);
		}
	}


	public void SetIndex(int i){
		index = i;
	}
	public int GetIndex(){
		return index;
	}

	/**
	 * What happens when this Item is used
	 */
	abstract protected void UseAction ();
}
