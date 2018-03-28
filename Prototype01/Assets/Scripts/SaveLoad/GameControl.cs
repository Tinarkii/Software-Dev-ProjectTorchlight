
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
	private GameObject player;
	public static string[] scenes = new string[]{"TitleScreen","sample","Room01"};
	private LevelData[] levels = new LevelData[12];
	public GameObject playerPrefab;


	private int door = 0;
	private Vector3? playerPosition = null;
	private bool doorOrPos = false;

	public int confidence;
	public string currentScene;


	void CreateEmptyLevels(){
		for (int i = 0; i < levels.Length; i++) {
			levels [i] = new LevelData ();
		}
	}

	void Awake () 
	{
		CreateEmptyLevels ();
		currentScene = SceneManager.GetActiveScene ().name;
		if (control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
			DontDestroyOnLoad (GameObject.Find ("Player"));
			player = GameObject.Find ("Player");
		}
		else if (control != this)
		{
			Destroy(gameObject);

		}

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject p in players){
			if (p != control.player) {
				Destroy (p);
			}
		}

	}

	public GameObject GetPlayer(){
		return player;
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
		Debug.Log ("Game Saved");

		data.currentScene = currentScene;
		data.levels = CacheLevelData();
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
			levels = data.levels;
			doorOrPos = false;
			SceneManager.LoadScene(currentScene);

			confidence = data.confidence;
			playerPosition = data.playerPosition;
			if (PlayerPrefs.HasKey("Health"))
				confidence = PlayerPrefs.GetInt("Health");

			Debug.Log(data.playerPosition);

		}
	}
		

	public void SwapScene(int sceneToLoad,int doorToLoad){
		CacheLevelData ();
		door = doorToLoad;
		doorOrPos = true;
		SceneManager.LoadScene (scenes [sceneToLoad]);

	}

	public Vector3 GetPlayerSpawn(){
		if (doorOrPos) {
			return GameObject.Find ("SceneControl").GetComponent<SceneControl> ().doors[door].GetComponent<Door>().GetSpawnPoint();
		} else {
			if (playerPosition == null) {
				return GameObject.Find ("SceneControl").GetComponent<SceneControl> ().neutralSpawnPoint;
			}
			return playerPosition.Value;
		}
	}

	public LevelData[] CacheLevelData(){
		LevelData level = levels [Array.IndexOf (scenes, currentScene)];
		level.bitLamps = GameObject.Find ("SceneControl").GetComponent<SceneControl> ().GetLamps();
		level.bitBaddies = 0;
		return levels;
	}

	public LevelData GetLevelCache(){
		Debug.Log(Array.IndexOf(scenes,currentScene));
		return levels [Array.IndexOf (scenes, currentScene)];
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
	public LevelData[] levels;
	public string currentScene;
}


[Serializable]
public class LevelData{
	public short bitLamps = 0;
	public short bitBaddies = 0;
}