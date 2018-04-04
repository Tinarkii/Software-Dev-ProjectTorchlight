using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {
	private GameObject player;
	public Vector3 neutralSpawnPoint;
	public GameObject[] doors = new GameObject[8];
	public GameObject[] lamps = new GameObject[16];
	public GameObject[] baddies = new GameObject[16];
	private short bitLamps = 0;
	private short bitBaddies = ~0;


	// Use this for initialization
	void Start () {
		player = GameControl.control.GetPlayer ();
		player.SetActive (true);
		player.transform.position = GameControl.control.GetPlayerSpawn();
		player.GetComponent<OverWorldNavOG> ().Cleanse ();
		GameObject.Find ("Camera").GetComponent<OverworldCameraMovement> ().Snap (player.transform.position);

		bitLamps = GameControl.control.GetLevelCache ().bitLamps;
		bitBaddies = GameControl.control.GetLevelCache ().bitBaddies;
		LoadLampArray ();
		LoadBaddieArray ();
	}

	public GameObject GetPlayer(){
		return player;
	}

	public void UpdateLamp(int index)
	{
		bitLamps |= (short)(1 << index);
		Debug.Log (index);
	}

	public short GetLamps(){
		return bitLamps;
	}

	public short GetBaddies(){
		return bitBaddies;
	}

	public void LoadLampArray()
	{
		Debug.Log ("Game Loaded : "+bitLamps.ToString("x4"));
		for (int i = 0; i < lamps.Length; i++) {
			bool state = ((bitLamps >> i) & (1)) > 0;
			string name = "Lamp (" + i + ")";
			if (GameObject.Find (name) != null) {
				Debug.Log ("Found " + i + ", " + state);
				lamps [i] = GameObject.Find (name);
				lamps [i].GetComponent<LampLightTest> ().SetIndex (i);
				lamps [i].GetComponent<LampLightTest> ().on = state;
			}
		}

	}

	public void LoadBaddieArray()
	{
		Debug.Log ("Game Loaded : "+bitBaddies.ToString("x4"));
		for (int i = 0; i < lamps.Length; i++) {
			bool state = ((bitBaddies >> i) & (1)) > 0;
			string name = "Baddie (" + i + ")";
			if (GameObject.Find (name) != null) {
				Debug.Log ("Baddie " + i + ", " + state);
				baddies [i] = GameObject.Find (name);
				baddies [i].GetComponent<Baddie> ().SetIndex (i);
				baddies [i].GetComponent<Baddie> ().alive = state;
			}
		}

	}

}
