using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	// Keep track of whether this script has finished its phase
	private int health;

	// Amount of time before phase ends. Might remove later
	private float timePassed;

	// Holds info for an incoming attack
	private struct Attack {
		// Time to initiate attack
		public float time;
		// The type of attack
		public enum type {Shield, Blast, Block, Tap};
		// The side of the screen it comes from
		public enum side {Left, Right};
	};

	// The attacks that will be produced on the enemy's turn
	// Note: might make this a 2D array so multiple sets of attacks
	// can be cycled through
	private List<Attack> attacks;
	
	/* Keeps track of whether the defensive phase has ended
	 */
	public bool Finished() {
		return timePassed >= 4f;
	}

	// Use this for initialization
	void Start () {
		// These should come from overworld
		health = 100;
		attacks = new List<Attack>();
	}

	// Update is called once per frame
	void Update () {
         timePassed += Time.deltaTime;
	}

	void OnEnable () {
		timePassed = 0;
	}
	
	public bool ToExit() {
		return health <= 0;
	}
}
