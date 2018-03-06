using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	// Keep track of whether this script has finished its phase
	private int health;

	// Amount of time before phase ends. Might remove later
	private float timeLeft;

	// Holds info for an incoming attack
	private struct attack {
	};

	// The attacks that will be produced on the enemy's turn
	// Note: might make this a 2D array so multiple sets of attacks
	// can be cycled through
	private attack[] attacks;
	
	/* Keeps track of whether the defensive phase has ended
	 */
	public bool Finished() {
		return timeLeft <= 0;
	}

	// Use this for initialization
	void Start () {
		health = 100;// This should come from overworld
	}

	void OnEnable () {
		timeLeft = 4f;
	}
	
	// Update is called once per frame
	void Update () {
         timeLeft -= Time.deltaTime;
	}

	public bool ToExit() {
		return health <= 0;
	}
}
