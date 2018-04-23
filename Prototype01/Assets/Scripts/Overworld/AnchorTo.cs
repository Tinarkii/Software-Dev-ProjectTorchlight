using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnchorTo : MonoBehaviour {
	public GameObject target;
	public Vector3 offset;
	// Use this for initialization
	void Start () {
		if(!EventSystem.current.IsPointerOverGameObject())
		target = GameControl.control.GetPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControl.control.GetPlayer () == null) {
			return;
		}
		transform.position = target.transform.position + offset;
	}
}
