using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Runtime.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class GameControl : MonoBehaviour {

	public static GameControl control;

	public int confidence;
	public Vector3 playerPosition;
	public bool lamp;


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

		SaveGame data = new SaveGame();
		V3S.SerializableVector3 serPlayerPos = GameObject.Find("Player").transform.position;
		Debug.Log(GameObject.Find("Player").transform.position);

		data.lamp = GameObject.Find("Lamp").GetComponent<LampLightTest>().on;
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

			lamp = data.lamp;
			confidence = data.confidence;
			playerPosition = data.playerPosition;
			GameObject.Find("Player").transform.position = data.playerPosition;
			GameObject.Find("Lamp").GetComponent<LampLightTest>().on = data.lamp;
			Debug.Log(data.playerPosition);

		}
	}

}

[Serializable] 
class SaveGame
{
	public int confidence;
	public V3S.SerializableVector3 playerPosition;
	public bool lamp;
}