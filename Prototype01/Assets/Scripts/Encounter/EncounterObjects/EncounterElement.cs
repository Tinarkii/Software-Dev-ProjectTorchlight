using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An abstract script for all shields, whether the player's or enemys'
 * @TODO: Is it worth making ShieldOfPlayer and ShieldOfEnemy just instances of this class?
 */

public abstract class EncounterElement : MonoBehaviour
{

	/**
	 * Every encounter element will have to handle collisions somehow
	 */
	protected abstract void OnCollisionEnter(Collision col);

}
