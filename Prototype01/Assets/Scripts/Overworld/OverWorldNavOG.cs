using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OverWorldNavOG : MonoBehaviour {
    Rigidbody self;
    Vector3 target;
	public int maxSpeed;
    RaycastHit hitPoint;
	public Camera usedCamera;
	private Vector3 veloc;
	private int bufferFrame = 0;

    // Use this for initialization
    void Start () {
		target = transform.position;
		veloc = new Vector3 (0, 0, 0);
        self = GetComponent<Rigidbody>();

	}

	public void Cleanse(){
		target = self.position;
		bufferFrame = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Ray ray = usedCamera.ViewportPointToRay(usedCamera.ScreenToViewportPoint(Input.mousePosition));
        if (Input.GetMouseButton(0))
        {
			if (Physics.Raycast (ray, out hitPoint, 1000, (1 << 9)) && bufferFrame < 1) {
				target = hitPoint.point;
			} else {
				bufferFrame--;
			}
        }

		veloc = (target - self.position) * (6.5f/7.0f);

		float speed = Mathf.Min (maxSpeed, veloc.magnitude);
           
		veloc = (veloc.normalized);
           
		veloc = Vector3.Scale (veloc, new Vector3 (speed, 0, speed));

		self.velocity = new Vector3 (veloc.x, self.velocity.y, veloc.z);
		if (veloc.magnitude > .1) {
			Vector3 lookto = new Vector3 (target.x, self.position.y, target.z);
			self.transform.LookAt (lookto);
		} else {
			target = transform.position;
			self.velocity = new Vector3(0,0,0);
		}

    }
}
