using UnityEngine;
using System.Collections;

/**
 * A script for blankets, which increase the player's maximum confidence
 */

public class Blanket : MaxConfidenceItem
{
    /**
     * Initalization: Set the name and maximum confidence increase
     */

    public void Start()
    {
        myName = "Blanket";
        bonusConfidence = 50;
        description = "Increases maximum confidence by " + bonusConfidence;
    }
}
