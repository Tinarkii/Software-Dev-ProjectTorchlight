using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A sample Item for testing purposes
 */

public class SampleItem : Item
{

	/**
	 * Initialization
	 */
	public void Start ()
	{
		myName = "SampleItem";
	}

	/**
	 * Use the SampleItem
	 */
	public override void UseItem ()
	{
		Debug.Log("SampleItem used");
		//@TODO: the below line doesn't work yet
		//Instantiate (this, Camera.Main.ScreenToWorldPoint (Vector3 (250, 250, 0)), Quaternion.Identity);
	}
	
}
