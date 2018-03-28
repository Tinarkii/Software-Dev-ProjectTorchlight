using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Game : MonoBehaviour{
	public GameObject[] lamps = new GameObject[16];
	private  short bitLamps = 0;
	private bool gameLoaded = false;

	// Use this for initialization
	void Start () {
		gameLoaded = false;

		for(int i=0; i < lamps.Length; i++){
			if (lamps [i] != null) {
				bitLamps += (short)(lamps [i].GetComponent<LampLightTest> ().CheckLight() << i);
				lamps [i].GetComponent<LampLightTest> ().SetIndex (i);
			}
		}

	}

	public void UpdateLamp(int index){
		bitLamps ^= (short)(1 << index);
		SaveGame ();
	}

	private Save CreateSaveGameObject()
	{
		Save save = new Save();

		save.lamps = bitLamps;

		return save;
	}

	public void SaveGame(){
	
		Save save = CreateSaveGameObject ();

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/gamesave.save");
		bf.Serialize (file, save);
		file.Close ();
			
		Debug.Log ("Game Saved : "+bitLamps.ToString("x4"));

	}

	public void LoadGame(){
		if (File.Exists (Application.persistentDataPath + "/gamesave.save")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/gamesave.save", FileMode.Open);
			Save save = (Save)bf.Deserialize (file);
			file.Close();

			bitLamps = save.lamps;

			for(int i=0; i < lamps.Length; i++){
				if (lamps [i] != null && ((bitLamps >> i) & (1)) > 0) {
					lamps [i].GetComponent<LampLightTest> ().myLight.enabled = true;
					Debug.Log ("Lamp " + i + " activated!");
				}
				else if(lamps[i] != null){
					lamps [i].GetComponent<LampLightTest> ().myLight.enabled = false;
				}
			}

		//	Debug.Log ("Game Loaded : "+bitLamps.ToString("x4"));



		}
	}

	public void WipeSave(){
		bitLamps = 0;
		SaveGame ();
		LoadGame ();

	}


	// Update is called once per frame
	void Update () {

		if (!gameLoaded) {
			LoadGame ();
			gameLoaded = true;
		}

		if(Input.GetKeyDown("q")){
			WipeSave();
		}


		
	}
}
