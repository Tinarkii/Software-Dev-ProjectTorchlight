﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : EncounterElement {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// A very hackish way to do move the object.
		// If someone would be so kind as to provide a less hackish solution,
		// it would be much appreciated.
		transform.Translate(Vector3.forward * 5 * Time.deltaTime);
	}
	
	protected override void OnCollisionEnter(Collision col) {

	}
}
