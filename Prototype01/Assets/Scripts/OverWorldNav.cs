using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OverWorldNav : MonoBehaviour {
    Rigidbody self;
    Vector3 target;
    public int maxSpeed;
    RaycastHit hitPoint;
    public Camera usedCamera;

    // Use this for initialization
    void Start () {
        target = new Vector3(0, 0, 0);
        self = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = usedCamera.ViewportPointToRay(usedCamera.ScreenToViewportPoint(Input.mousePosition));
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hitPoint,500))
            {

                target = hitPoint.point;




            }
        }

        Vector3 veloc = (target - self.position) * (8.0f/7.0f);
        //Debug.Log("Target: " + target + " Velocity: " + veloc);

        if (veloc.magnitude > maxSpeed)
        {
            veloc = (veloc.normalized);
            veloc = Vector3.Scale(veloc, new Vector3(maxSpeed, 0, maxSpeed));
        }

        self.velocity = new Vector3(veloc.x, self.velocity.y, veloc.z);
	



    }
}
