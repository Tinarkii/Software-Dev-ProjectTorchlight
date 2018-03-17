using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorTo : MonoBehaviour {
	public GameObject target;
	public Vector3 offset;
	// Use this for initialization
	void Start () {
		transform.position = target.transform.position + offset;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = target.transform.position + offset;
	}
}
