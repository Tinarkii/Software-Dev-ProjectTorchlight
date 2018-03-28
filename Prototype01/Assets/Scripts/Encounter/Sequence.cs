using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence {

	// Reference to the singleton class
	private static Sequence rf;

	// Holds info for an incoming attack
	public struct Attack {
		// The type of attack
		public Transform type;
		// Time to initiate attack
		public float time;
		// Where the object will be spawned
		public int height;
		// Speed of object; also determines which side of screen
		public float velocity;

		// Constructor to make coding faster
		public Attack(Transform type, float time, int height, float velocity) {
			this.type = type;
			this.time = time;
			this.height = height;
			this.velocity = velocity;
		}

		// End of sequence null constructor
		public Attack(float time) {
			this.type = null;
			this.time = time;
			this.height = 0;
			this.velocity = 0;
		}
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
				return ID00();
			case 1:
				return ID01();
			case 2:
			return ID02();
			default:
				throw new System.ArgumentException("Parameter is not in range of enemy IDs", "sequence");
		}
	}

	// # Begin region of builders for enemy attack sequences
	// Currently I end with a null attack to signal the end of an attack sequence.
	// Not the prettiest way to do it, and should be rethought later on.
	// Also, I really should construct the queues inside a for loop over a text file or something.

	private Queue<Attack> ID00() {
		Queue<Attack> attacks = new Queue<Attack>();

		attacks.Enqueue(new Attack(enemyBlast, 0f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 3, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 4, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 5, -5));

		attacks.Enqueue(new Attack(enemyShield, 2f, 0, 5));
		attacks.Enqueue(new Attack(enemyShield, 2f, 2, -5));
		attacks.Enqueue(new Attack(enemyShield, 2f, 4, 5));

		attacks.Enqueue(new Attack(enemyBlock, 4f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 4.3f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 4.6f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 4.9f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 5.2f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlock, 5.5f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlock, 5.8f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlock, 6.1f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlock, 6.4f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlock, 6.7f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlock, 7.0f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlock, 7.3f, 2, 5));

		attacks.Enqueue(new Attack(11));
		return attacks;
	}

	private Queue<Attack> ID01() {
		Queue<Attack> attacks = new Queue<Attack>();

		attacks.Enqueue(new Attack(2));

		return attacks;
	}

	private Queue<Attack> ID02() {
		Queue<Attack> attacks = new Queue<Attack>();

		attacks.Enqueue(new Attack(enemyBlast, 0f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 2, -5));

		attacks.Enqueue(new Attack(enemyBlast, 0.5f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0.5f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0.5f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0.5f, 2, -5));

		attacks.Enqueue(new Attack(enemyShield, 2f, 0, 5));

		attacks.Enqueue(new Attack(enemyBlock, 2f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 2.3f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 2.6f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 2.9f, 0, 5));

		attacks.Enqueue(new Attack(enemyShield, 3f, 2, -5));
		attacks.Enqueue(new Attack(enemyShield, 3f, 4, 5));


		attacks.Enqueue(new Attack(7));
		return attacks;
	}
}
