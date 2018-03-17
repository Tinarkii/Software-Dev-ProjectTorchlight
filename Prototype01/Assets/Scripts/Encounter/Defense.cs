using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	private bool finished;

	// Keep track of whether this script has finished its phase
	private int health;

	// Amount of time before phase ends. Might remove later
	private float timePassed;

	// Holds info for an incoming attack
	private struct Attack {
		// Time to initiate attack
		public float time;
		// Where the object will be spawned
		public int height;
		// The type of attack
		public Transform type;
		// The side of the screen it comes from
		public bool leftSide;
	};

	// The attacks that will be produced on the enemy's turn
	private Queue<Attack> attacks;

	// To hold the objects to be spawned
	public Transform enemyShield;
	public Transform enemyBlast;
	public Transform enemyBlock;
	

	/* Keeps track of whether the defensive phase has ended
	 */
	public bool Finished() {
		// Need to double check how the initialization of var attacks works with this
		return finished;
	}

	// Use this for initialization
	void Start () {
		// These should defined by a parameter; these are testing values
		attacks = new Queue<Attack>();
		attacks.Enqueue(new Attack{time = 2, height = 0, type = enemyBlast, leftSide = true});
		// Currently I end with a null attack to signal the end of an attack sequence.
		// Not the prettiest way to do it, and should be rethought later on.
		// Probably a variable that counts off a second or so after attacks.Count == 0
		attacks.Enqueue(new Attack{time = 1});
	}

	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		while (attacks.Count > 0 && attacks.Peek().time <= timePassed) {
			// This needs to be finished; is test version
			Attack att = attacks.Dequeue();
			if (att.type != null)
				Instantiate(att.type, new Vector3(0, 0, 0), Quaternion.identity);
		}

		finished = (2 - timePassed) <= 0;
	}

	void OnEnable () {
		finished = false;
		timePassed = 0;
	}

	public void SetHealth(int health) {
		this.health = health;
	}
	
	public bool ToExit() {
		return health <= 0;
	}
}
