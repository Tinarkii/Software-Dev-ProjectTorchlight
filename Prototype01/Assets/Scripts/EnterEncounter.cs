using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterEncounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		 if (other.gameObject.tag == "Player")
		 	SceneManager.LoadScene("sampleEncounter");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
