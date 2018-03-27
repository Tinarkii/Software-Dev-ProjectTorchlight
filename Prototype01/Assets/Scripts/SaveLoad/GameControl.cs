
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Runtime.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameControl : MonoBehaviour {

	public static GameControl control;

	public GameObject[] lamps = new GameObject[16];
	private  short bitLamps = 0;

	public int confidence;
	public Vector3 playerPosition;
	public string currentScene;

	private int frameBuffer = 1;


	void Awake () 
	{
		if (control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this)
		{
			Destroy(gameObject);
		}



	}

	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 30), "Confidence: " + confidence);
	}

	public void Save()
	{
		

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savegame.dat");
		currentScene = SceneManager.GetActiveScene ().name;
		SaveGame data = new SaveGame();
		V3S.SerializableVector3 serPlayerPos = GameObject.Find("Player").transform.position;
		Debug.Log(GameObject.Find("Player").transform.position);
		Debug.Log ("Game Saved : "+bitLamps.ToString("x4"));

		data.currentScene = currentScene;
		data.lamps = bitLamps;
		data.confidence = confidence;
		data.playerPosition = serPlayerPos;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/savegame.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savegame.dat", FileMode.Open);
			SaveGame data = (SaveGame)bf.Deserialize(file);
			file.Close();

			currentScene = data.currentScene;
			SceneManager.LoadScene(currentScene);
			bitLamps = data.lamps;
			confidence = data.confidence;
			playerPosition = data.playerPosition;
			if (PlayerPrefs.HasKey("Health"))
				confidence = PlayerPrefs.GetInt("Health");

			frameBuffer = 1;
			Debug.Log(data.playerPosition);

		}
	}


	public void UpdateLamp(int index)
	{
		bitLamps ^= (short)(1 << index);
		Debug.Log (index);
	}

	public void LoadArray()
	{
		Debug.Log ("Game Loaded : "+bitLamps.ToString("x4"));
		for (int i = 0; i < lamps.Length; i++) {
			bool state = ((bitLamps >> i) & (1)) > 0;
			String name = "Lamp (" + i + ")";
			if (GameObject.Find (name) != null) {
				Debug.Log ("Found " + i + ", " + state);
				lamps [i] = GameObject.Find (name);
				lamps [i].GetComponent<LampLightTest> ().SetIndex (i);
				lamps [i].GetComponent<LampLightTest> ().on = state;
			}
		}
	}


	void Update () 
	{
		if (frameBuffer == 0) 
		{
			LoadArray ();
			GameObject.Find("Player").transform.position = playerPosition;
			GameObject.Find ("Player").GetComponent<OverWorldNavOG> ().Cleanse ();
			GameObject.Find ("Camera").GetComponent<OverworldCameraMovement> ().Snap ();
		}
		frameBuffer--;

	}


}

/// <summary>
/// The contents saved in our savegame.dat file
/// </summary>
[Serializable] 
class SaveGame
{
	public int confidence;
	public V3S.SerializableVector3 playerPosition;
	public short lamps;
	public string currentScene;
}
