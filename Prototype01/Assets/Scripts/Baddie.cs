using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour {

	public SphereCollider collider; 

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter (Collider coll){
		BroadcastMessage ("initateEncounter"); 
	}
	
	// Update is called once per frame
	void Update () {
		OnTriggerEnter (collider); 
	}
}
