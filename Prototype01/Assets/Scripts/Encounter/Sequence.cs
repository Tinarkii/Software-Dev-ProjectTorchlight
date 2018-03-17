using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence {

	// Reference to the singleton class
	private static Sequence rf;

	// Holds info for an incoming attack
	public struct Attack {
		// Time to initiate attack
		public float time;
		// Where the object will be spawned
		public int height;
		// The type of attack
		public Transform type;
		// The side of the screen it comes from
		public bool leftSide;
	};
	
	// To hold the objects to be spawned
	private static Transform enemyShield;
	private static Transform enemyBlast;
	private static Transform enemyBlock;


	/* Private constructor for singleton class */
	private Sequence() {
		Defense temp = Camera.main.GetComponent<Defense>();
		enemyBlast = temp.enemyBlast;
		enemyBlock = temp.enemyBlock;
		enemyShield = temp.enemyShield;
	}

	/* Static initializer */
	static Sequence() {
		rf = new Sequence();
	}

	/* Give refernce to signleton class */
	public static Sequence getRef() {
		return rf;
	}

	/* Create the moves for an attack sequence */
	public Queue<Attack> getMoves(int enemyID, int sequence) {
		switch (enemyID) {
			case 0:
				return ID0();
			case 1:
				return ID1();
			default:
				throw new System.ArgumentException("Parameter is not in range of enemy IDs", "sequence");
		}
	}

	// # Begin region of builders for enemy attack sequences
	// Currently I end with a null attack to signal the end of an attack sequence.
	// Not the prettiest way to do it, and should be rethought later on.
	// Probably a variable that counts off a second or so after attacks.Count == 0
	// Also, I may want to construct the queues inside a for loop over a text file or something.

	private Queue<Attack> ID0() {
		Queue<Attack> attacks = new Queue<Attack>();
		attacks.Enqueue(new Attack{time = 0.75f, height = 0, type = enemyBlast, leftSide = true});
		attacks.Enqueue(new Attack{time = 0.85f, height = 0, type = enemyBlock, leftSide = true});
		attacks.Enqueue(new Attack{time = 0.95f, height = 0, type = enemyShield, leftSide = true});
		attacks.Enqueue(new Attack{time = 2});

		return attacks;
	}

	private Queue<Attack> ID1() {
		Queue<Attack> attacks = new Queue<Attack>();
		attacks.Enqueue(new Attack{time = 1});

		return attacks;
	}
}
