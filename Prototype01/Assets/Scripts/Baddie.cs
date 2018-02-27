using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour {

	public Collider collider; 

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter (){
		//BroadcastMessage ("initateEncounter"); 
	}
	
	// Update is called once per frame
	void Update () {
		OnCollisionEnter (); 
	}
}
