using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {
    Rigidbody self;
    Vector3 target;
    public int power;

	// Use this for initialization
	void Start () {
        target = new Vector3(0, 0, 0);
        self = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButton(0))
        {
            target.x = Input.mousePosition.x - (Screen.width/2);
            target.z = Input.mousePosition.y - (Screen.height/2);

        }
        else
        {
            target = new Vector3(0, 0, 0);
        }

        target.Normalize();
        target.Scale(new Vector3(power, power, power));


        self.AddForce(target);

        Debug.Log(Input.mousePosition.ToString());


    }
}
