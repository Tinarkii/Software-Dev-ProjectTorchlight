using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {
	private GameObject player;
	public Vector3 neutralSpawnPoint;
	public GameObject[] doors = new GameObject[8];
	public GameObject[] lamps = new GameObject[16];
	public GameObject[] baddies = new GameObject[16];
	public GameObject[] items = new GameObject[16];
	private short bitLamps = 0;
	private short bitBaddies = ~0;
	private short bitItems = 0;


	// Use this for initialization
	void Start () {
		player = GameControl.control.GetPlayer ();
		player.SetActive (true);
		player.transform.position = GameControl.control.GetPlayerSpawn();
		player.GetComponent<OverWorldNavOG> ().Cleanse ();
		GameObject.Find ("Camera").GetComponent<OverworldCameraMovement> ().Snap (player.transform.position);

		bitLamps = GameControl.control.GetLevelCache ().bitLamps;
		bitBaddies = GameControl.control.GetLevelCache ().bitBaddies;
		bitItems = GameControl.control.GetLevelCache ().bitItems;
		LoadLampArray ();
		LoadBaddieArray ();
		LoadItemArray ();
	}

	public GameObject GetPlayer(){
		return player;
	}

	public void UpdateLamp(int index)
	{
		bitLamps |= (short)(1 << index);
	}

	public void UpdateItem(int index)
	{
		bitItems |= (short)(1 << index);
		Debug.Log (index);
	}

	public short GetLamps(){
		return bitLamps;
	}

	public short GetBaddies(){
		return bitBaddies;
	}

	public short GetItems(){
		return bitItems;
	}

	public void LoadLampArray()
	{
		for (int i = 0; i < lamps.Length; i++) {
			bool state = ((bitLamps >> i) & (1)) > 0;
			string name = "Lamp (" + i + ")";
			if (GameObject.Find (name) != null) {
				lamps [i] = GameObject.Find (name);
				lamps [i].GetComponent<LampLightTest> ().SetIndex (i);
				lamps [i].GetComponent<LampLightTest> ().on = state;
			}
		}

	}

	public void LoadBaddieArray()
	{
		for (int i = 0; i < baddies.Length; i++) {
			bool state = ((bitBaddies >> i) & (1)) > 0;
			string name = "Baddie (" + i + ")";
			if (GameObject.Find (name) != null) {
				baddies [i] = GameObject.Find (name);
				baddies [i].GetComponent<Baddie> ().SetIndex (i);
				baddies [i].GetComponent<Baddie> ().alive = state;
			}
		}

	}

	public void LoadItemArray()
	{
		for (int i = 0; i < items.Length; i++) {
			bool state = ((bitItems >> i) & (1)) > 0;
			string name = "Item (" + i + ")";
			if (GameObject.Find (name) != null) {
				Debug.Log ("Item " + i + ", " + state);
				items [i] = GameObject.Find (name);
				items [i].GetComponent<Item> ().SetIndex (i);
				items [i].GetComponent<Item> ().picked = state;
			}
		}

	}

}
