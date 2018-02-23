using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A sample Item for testing purposes
 */

public class SampleItem : Item
{

	/**
	 * Use the SampleItem
	 */
	public override void useItem ()
	{
		Debug.Log("SampleItem used");
		//@TODO: this item could do something more interesting
	}
	
}
