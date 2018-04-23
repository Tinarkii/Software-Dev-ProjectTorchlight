using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class that provides attack sequences for enemy encounters */
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
	public Queue<Attack> getMoves(string enemy) {
		// For random sets of attacks
		System.Random r = new System.Random();

		///@TODO: This works, but it's kinda messy (e.g., if a new enemy is made, the code here will need to be changed).
		// Is there a better way of doing it?
		switch (enemy) {

			case "armorBaddie":
				int rVal = r.Next(0, 3);
				if (rVal == 0)
					return Shields();
				else if (rVal == 1)
					return Blocks();
				else if (rVal == 2)
					return ArmorOG();
				else
					throw new IndexOutOfRangeException();// Should never happen; checking for coding errors

			case "crystalBaddie":
				if (r.Next(0, 2) == 0)
					return Blasts();
				else
					return CrystalOG();

			default:
				throw new System.ArgumentException("Parameter is not in range of enemys", "enemy");
		}
	}

	// # Begin region of builders for enemy attack sequences

	private Queue<Attack> Blasts() {
		Queue<Attack> attacks = new Queue<Attack>();

		attacks.Enqueue(new Attack(enemyBlast, 0f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 2, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 3, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 3, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 4, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 4, 5));

		attacks.Enqueue(new Attack(enemyBlock, 1.8f, 0, -5));

		attacks.Enqueue(new Attack(enemyBlast, 3.2f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlast, 3.5f, 1, 5));

		return attacks;
	}

	private Queue<Attack> Shields() {
		Queue<Attack> attacks = new Queue<Attack>();

		attacks.Enqueue(new Attack(enemyShield, 0f, 0, -5));
		attacks.Enqueue(new Attack(enemyShield, 0f, 0, 5));
		attacks.Enqueue(new Attack(enemyShield, 0f, 2, -5));
		attacks.Enqueue(new Attack(enemyShield, 0f, 2, 5));

		attacks.Enqueue(new Attack(enemyBlock, 1.8f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlock, 2.1f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlock, 2.4f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlock, 2.7f, 0, -5));

		attacks.Enqueue(new Attack(enemyShield, 3.2f, 2, -5));

		return attacks;
	}

	private Queue<Attack> Blocks() {
		Queue<Attack> attacks = new Queue<Attack>();

		attacks.Enqueue(new Attack(enemyBlock, 0f, 4, -5));
		attacks.Enqueue(new Attack(enemyBlock, 0f, 4, 5));
		attacks.Enqueue(new Attack(enemyBlock, 0.3f, 4, -5));
		attacks.Enqueue(new Attack(enemyBlock, 0.3f, 4, 5));
		attacks.Enqueue(new Attack(enemyBlock, 0.6f, 4, -5));
		attacks.Enqueue(new Attack(enemyBlock, 0.6f, 4, 5));

		attacks.Enqueue(new Attack(enemyBlock, 0.6f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlock, 0.9f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlock, 1.2f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlock, 1.5f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlock, 1.8f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlock, 2.1f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlock, 2.4f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlock, 2.7f, 1, -5));
		attacks.Enqueue(new Attack(enemyBlock, 3.0f, 2, -5));
		attacks.Enqueue(new Attack(enemyBlock, 3.3f, 2, -5));
		attacks.Enqueue(new Attack(enemyBlock, 3.6f, 2, -5));
		attacks.Enqueue(new Attack(enemyBlock, 3.9f, 2, -5));

		return attacks;
	}

	private Queue<Attack> ArmorOG() {
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

		return attacks;
	}

	private Queue<Attack> CrystalOG() {
		Queue<Attack> attacks = new Queue<Attack>();

		attacks.Enqueue(new Attack(enemyBlast, 0f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0f, 2, -5));

		attacks.Enqueue(new Attack(enemyBlast, 0.8f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0.8f, 0, -5));
		attacks.Enqueue(new Attack(enemyBlast, 0.8f, 2, 5));
		attacks.Enqueue(new Attack(enemyBlast, 0.8f, 2, -5));

		attacks.Enqueue(new Attack(enemyShield, 2f, 0, 5));

		attacks.Enqueue(new Attack(enemyBlock, 2f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 2.3f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 2.6f, 0, 5));
		attacks.Enqueue(new Attack(enemyBlock, 2.9f, 0, 5));

		attacks.Enqueue(new Attack(enemyShield, 3f, 2, -5));
		attacks.Enqueue(new Attack(enemyShield, 3f, 4, 5));

		return attacks;
	}
}
