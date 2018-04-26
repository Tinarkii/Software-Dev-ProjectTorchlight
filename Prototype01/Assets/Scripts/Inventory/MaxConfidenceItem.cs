using UnityEngine;
using System.Collections;

/**
 * Superclass for items that increase maximum confidence
 */

public abstract class MaxConfidenceItem : Item
{
    /**
     * How much maximum confidence should be increased by
     */
    protected int bonusConfidence;

    /**
     * Use the MaxConfidenceItem - increases the player's max confidence
     */
    protected override void UseAction()
    {
        Debug.Log("A " + myName + " was used. The player's maximum confidence should be increased by " + bonusConfidence + ".");
        GameControl.control.AdjustMaxConfidenceBy(bonusConfidence);
    }
}
