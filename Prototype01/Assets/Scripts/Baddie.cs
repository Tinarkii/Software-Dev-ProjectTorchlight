using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Baddie : MonoBehaviour { //class to control the baddie, will eventually be repsonsible for patrol routes 
									  //and sight and moving towards player and other functions the baddies need to run

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter(Collision other){ //if object this is tagged to is collided 
		if (other.gameObject.tag == "Player") {//and if the player object is the one that collided with it 
			SceneManager.LoadScene ("sampleEncounter"); //switch into encounter mode 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
