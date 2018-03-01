using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

	// Keep track of whether this script has finished its phase
	private int health;

	private float time;
	
	/* Keeps track of whether the defensive phase has ended
	 */
	public bool Finished() {
		return time <= 0;
	}

	// Use this for initialization
	void Start () {
		health = 100;// This should come from overworld
	}

	void OnEnable () {
		time = 4f;
	}
	
	// Update is called once per frame
	void Update () {
         time -= Time.deltaTime;
	}

	public bool ToExit() {
		return health <= 0;
	}
}
