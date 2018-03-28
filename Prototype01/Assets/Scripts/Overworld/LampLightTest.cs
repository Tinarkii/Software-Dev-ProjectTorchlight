using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLightTest : MonoBehaviour {

    public Light myLight; //the light we want to modify

    public bool on = false; //if the light is on 

	private int index = -1;

	public GameObject scene;
    

	// Use this for initialization
	void Start () {
        myLight.enabled = on; //turns the light off
		scene = GameObject.Find ("SceneControl");
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
			on = !on; //flips  the sate of the light
			scene.GetComponent<SceneControl> ().UpdateLamp (index);
        }
            
    }

	public void SetIndex(int i){
		index = i;
	}

	public int CheckLight(){return (on ? 1 : 0);}

    // Update is called once per frame
    void Update () {
		if (scene == null)
			scene = GameObject.Find ("SceneControl");
		myLight.enabled = on;

	}
}
