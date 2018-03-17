using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLightTest : MonoBehaviour {

    public Light myLight; 

	// Use this for initialization
	void Start () {
        myLight.enabled = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            myLight.enabled = true; 
        }
            
    }

    // Update is called once per frame
    void Update () {
		
	}
}
