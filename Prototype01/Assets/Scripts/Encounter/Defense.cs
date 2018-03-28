using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	// Whether the phase has completed
	private bool finished;
	// Amount of time before phase ends. Might remove later
	private float timePassed;

	// Which of the enemy's attacks will be used
	private int seqenceNum;

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

	/* Create game objects representing the enemy's moves */
	private void Create(Sequence.Attack att) {
		int rev = (att.velocity > 0) ? 1 : -1;
		float adjust = (att.type == enemyShield) ? 0.5f : 0; // Not a fan of this, but it works
		// Too many magic numbers. I know this is a fantasy game, but still.
		Instantiate(att.type, new Vector3(rev*-11, (6.25f/4)*(att.height + adjust) + 0.2f, -3), Quaternion.Euler(0, 90*rev, 0));
	}

	void OnEnable () {
		finished = false;
		timePassed = 0;

		//@TODO: This works, but it's kinda messy (e.g., if a new enemy is made, the code here will need to be changed). Is there a better way of doing it?
		if (EncounterControl.enemyPrefab.tag == "armorBaddie")
			attacks = Sequence.getRef().getMoves(0, seqenceNum);
		else if (EncounterControl.enemyPrefab.tag == "crystalBaddie")
			attacks = Sequence.getRef().getMoves(1, seqenceNum);
		else
			Debug.LogError ("This enemy's type is not recognized: " + EncounterControl.enemyPrefab.tag);
		// Should increment sequenceNum, but I need to make it work in Sequence.cs first
	}

	/* Exit scene when player runs out of health */
	public bool ToExit() {
		return GameControl.control.confidence <= 0;
	}

	/* Keeps track of whether the defensive phase has ended */
	public bool Finished() {
		return finished;
	}
}
