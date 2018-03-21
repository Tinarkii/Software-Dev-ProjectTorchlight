using System.Collections;
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

	public int confidence;
	public Vector3 playerPosition;
	public Vector3 cameraPosition;
	public string currentScene;


	void Awake () 
	{
		currentScene = SceneManager.GetActiveScene ().name;
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
		V3S.SerializableVector3 serCameraPosition = GameObject.Find("Camera").transform.position;
		Debug.Log(GameObject.Find("Player").transform.position);

		data.currentScene = currentScene;
		data.cameraPosition = serCameraPosition;
		data.confidence = confidence;
		data.playerPosition = serPlayerPos;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/savegame.dat"))
		{
			if (SceneManager.GetActiveScene ().name.CompareTo(currentScene) > 0)	
				SceneManager.LoadScene(currentScene);
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savegame.dat", FileMode.Open);
			SaveGame data = (SaveGame)bf.Deserialize(file);
			file.Close();

			
			confidence = data.confidence;
			playerPosition = data.playerPosition;
			cameraPosition = data.cameraPosition;
			GameObject.Find("Camera").transform.position = data.cameraPosition;
			GameObject.Find("Player").transform.position = data.playerPosition;
			if (PlayerPrefs.HasKey("Health"))
				confidence = PlayerPrefs.GetInt("Health");
			
			Debug.Log(data.playerPosition);

		}
	}

}

[Serializable] 
class SaveGame
{
	public V3S.SerializableVector3 cameraPosition;
	public int confidence;
	public V3S.SerializableVector3 playerPosition;
	public string currentScene;
}