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
		// This is a horrible break of encapsulation and
		// should be dealt with when possible. In other words,
		// let's fix it once it works.
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
		// These should defined by parameters; these are testing values
		Queue<Attack> attacks = new Queue<Attack>();
		attacks.Enqueue(new Attack{time = 0.75f, height = 0, type = enemyBlast, leftSide = true});
		attacks.Enqueue(new Attack{time = 0.85f, height = 0, type = enemyBlock, leftSide = true});
		attacks.Enqueue(new Attack{time = 0.95f, height = 0, type = enemyShield, leftSide = true});
		// Currently I end with a null attack to signal the end of an attack sequence.
		// Not the prettiest way to do it, and should be rethought later on.
		// Probably a variable that counts off a second or so after attacks.Count == 0
		attacks.Enqueue(new Attack{time = 2});

		return attacks;
	}
}
