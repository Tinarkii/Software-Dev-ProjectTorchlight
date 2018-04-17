using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	// Whether the phase has completed
	private bool finished;

	// Amount of time before phase ends. Might remove later
	private float timePassed;

	// Delay let all attacks clear the screen
	private float timeLeft;

	// The attacks that will be produced on the enemy's turn
	private Queue<Sequence.Attack> attacks;

	// The objects that will be instantiated
	public Transform enemyShield;
	public Transform enemyBlast;
	public Transform enemyBlock;
	

	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		while (attacks.Count > 0 && attacks.Peek().time <= timePassed) {
			Sequence.Attack att = attacks.Dequeue();
			if (att.type != null)
				Create(att);
		}

		// Condition for exiting phase
		if (attacks.Count == 0)
			timeLeft -= Time.deltaTime;
		if (timeLeft <= 0)
			finished = true;
	}

	/* Create game objects representing the enemy's moves */
	private void Create(Sequence.Attack att) {
		int rev = (att.velocity > 0) ? 1 : -1;
		float adjust = (att.type == enemyShield) ? 0.5f : 0; // Not a fan of this, but it works
		// Too many magic numbers. I know this is a fantasy game, but still.
		Instantiate(att.type, new Vector3(rev*-11, (6.5f/4)*(att.height + adjust) + 0.2f, -3), Quaternion.Euler(0, 90*rev, 0));
	}

	void OnEnable () {
		finished = false;
		timePassed = 0;
		timeLeft = 4f;
		attacks = Sequence.getRef().getMoves(EncounterControl.enemyPrefab.tag);
	}

	/* Keeps track of whether the defensive phase has ended */
	public bool Finished() {
		return finished;
	}
}
