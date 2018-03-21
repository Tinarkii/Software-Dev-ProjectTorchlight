using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLightTest : MonoBehaviour {

    public Light myLight;
    private bool on = false; 
    

	// Use this for initialization
	void Start () {
        myLight.enabled = on;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            myLight.enabled = true;
            on = true; 
        }
            
    }

    // Update is called once per frame
    void Update () {
		
	}
}
