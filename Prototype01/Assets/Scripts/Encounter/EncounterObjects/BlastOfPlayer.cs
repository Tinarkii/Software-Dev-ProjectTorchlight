using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOfPlayer : MonoBehaviour {

    private int time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// called once per frame
	void FixedUpdate () {
		// A very hackish way to do move the object. (Also in other blast class.)
		// If someone would be so kind as to provide a less hackish solution,
		// it would be much appreciated.
		transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        time++;
        if(time >= 200)
        {
            Destroy(transform.gameObject);
            time = 0;
        }
	}

	protected void OnTriggerEnter(Collider col) {
        //A player's blast doesn't care about collisions that are not an enemy's shield
        if (col.gameObject.name != "ShieldBad(Clone)")
            return;

        Destroy(col.gameObject);
        Destroy(transform.gameObject);
    }
}
