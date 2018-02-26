using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWorldControl : MonoBehaviour {

	public GameObject gameBaddie;
	public Scene sampleEncounter; 

	// Use this for initialization
	void Start () {
		
	}
		
	void initateEncounter(){
		SceneManager.LoadScene("sampleEncounter"); 
	}
		

	// Update is called once per frame
	void Update () {
		initateEncounter ();
	}
}
