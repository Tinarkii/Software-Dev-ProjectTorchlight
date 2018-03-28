using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public int sceneToLoad;
	public int doorToLoad;
	public Vector3 spawnPoint = new Vector3(0,3,0);

	void OnTriggerEnter(Collider other){
			GameControl.control.SwapScene (sceneToLoad,doorToLoad);
	}

	public Vector3 GetSpawnPoint(){
		return spawnPoint;
	}
}
