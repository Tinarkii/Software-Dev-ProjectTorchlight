using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLightTest : MonoBehaviour {

    public Light myLight;
    private bool on = false; 
	private int index = -1;
	public GameObject game;
    

	// Use this for initialization
	void Start () {
        myLight.enabled = on;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			myLight.enabled = !myLight.enabled;
			on = myLight.enabled;

			game.GetComponent<Game> ().UpdateLamp (index);
        }
            
    }

	public void SetIndex(int i){
		index = i;
	}

	public int CheckLight(){return (on ? 1 : 0);}

    // Update is called once per frame
    void Update () {
		
	}
}
