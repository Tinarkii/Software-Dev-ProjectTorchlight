using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastOfEnemy : MonoBehaviour {

    private int time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// A very hackish way to do move the object. (Also in other blast class.)
		// If someone would be so kind as to provide a less hackish solution,
		// it would be much appreciated.
		transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        time++;
        if (time >= 300)
        {
            Destroy(transform.gameObject);
            time = 0;
        }
    }

	protected void OnTriggerEnter(Collider col) {
        //An enemy's blast doesn't care about collisions that are not a player's shield
        if (col.gameObject.name != "Shield(Clone)")
            return;

        Destroy(col.gameObject);
        Destroy(transform.gameObject);
    }
}
