using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	// Whether the phase has completed
	private bool finished;

	// Keep track of whether this script has finished its phase
	private int health;

	// Amount of time before phase ends. Might remove later
	private float timePassed;

	// Which of the enemy's attacks will be used
	int seqenceNum;

	// The attacks that will be produced on the enemy's turn
	private Queue<Sequence.Attack> attacks;

	// The objects that will be instantiated
	public Transform enemyShield;
	public Transform enemyBlast;
	public Transform enemyBlock;
	

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		while (attacks.Count > 0 && attacks.Peek().time <= timePassed) {
			// This needs to be finished; is test version
			Sequence.Attack att = attacks.Dequeue();
			if (att.type != null)
				Instantiate(att.type, new Vector3(0, 0, 0), Quaternion.identity);
		}

		if (attacks.Count == 0)
			finished = true;
	}

	void OnEnable () {
		finished = false;
		timePassed = 0;
		attacks = Sequence.getRef().getMoves(SceneParameters.enemyID, 0/*seqenceNum++*/);
	}

	public void SetHealth(int health) {
		this.health = health;
	}
	
	public bool ToExit() {
		return health <= 0;
	}

	/* Keeps track of whether the defensive phase has ended */
	public bool Finished() {
		return finished;
	}
}
