using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	// Whether the phase has completed
	private bool finished;
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
	void Start () {
		seqenceNum = 0;
	}

	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		while (attacks.Count > 0 && attacks.Peek().time <= timePassed) {
			Sequence.Attack att = attacks.Dequeue();
			if (att.type != null)
				Create(att);
		}

		if (attacks.Count == 0)
			finished = true;
	}

	void OnEnable () {
		finished = false;
		timePassed = 0;
		attacks = Sequence.getRef().getMoves(SceneParameters.enemyID, seqenceNum);
		// Should increment sequenceNum, but I need to make it work in Sequence.cs first
	}

	/* Exit scene when player runs out of health */
	public bool ToExit() {
		return SceneParameters.playerHealth <= 0;
	}

	/* Keeps track of whether the defensive phase has ended */
	public bool Finished() {
		return finished;
	}

	/* Create game objects representing the enemy's moves */
	private void Create(Sequence.Attack att) {
		// This needs to be finished, creating each object at the correct position and orientation
		Instantiate(att.type, new Vector3(0, 0, 0), Quaternion.identity);
	}
}
