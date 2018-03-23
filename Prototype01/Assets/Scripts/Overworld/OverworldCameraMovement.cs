using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCameraMovement : MonoBehaviour {

	Rigidbody self;
	public Rigidbody boy;
	Vector3 target;
	public int maxSpeed;
	public int minSpeed;

	// Use this for initialization
	void Start () {
		target = new Vector3(0, 0, 0);
		self = GetComponent<Rigidbody>();

	}

	public void Snap (){
		self.position = boy.transform.position + new Vector3(0,60,45);
	}

	// Update is called once per frame
	void Update ()
	{
		target = boy.transform.position;

		target.z += 45;

		target += (boy.transform.forward * 5 + boy.velocity);

		target.y = boy.transform.position.y + 60;

		Vector3 veloc = (target - self.position);

		float speed = Mathf.Min (maxSpeed, veloc.magnitude);

		veloc = (veloc.normalized);
		veloc *= speed;

		self.velocity = veloc;


	}
}
