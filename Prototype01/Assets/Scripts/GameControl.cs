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


/// <summary>
/// The Big Daddy System. Roughly speaking, this
/// is our "Control" or "Logic" class. It
/// determines what stays alive on scene
/// transition, facilitates saving and loading,
/// as well as maintains a soft save (or cache)
/// for transitioning between different scenes.
/// </summary>
public class GameControl : MonoBehaviour {


	private PlayerData playerData;
	public static GameControl control;
	private GameObject player;
	public static string[] scenes = new string[]{"TitleScreen","sample","Room01", "Room02", "Room03", "Room04"};
	private LevelData[] levels = new LevelData[12];
	public GameObject playerPrefab;


	private int door = 0;
	private int baddieToDie = -1;
	private Vector3? playerPosition = null;
	private bool doorQuery = false;

	public string currentScene;

	/**
	 * How much confidence (which is basically health) the player has
	 */
	private int confidence;

	/**
	 * The maximum amout of confidence the player can have
	 */
	private int maxConfidence = 100;


	/**
	 * How much damage enemy attacks cause to the player
	 */
	private int damageToPlayer;

	/**
	 * How much damage player attacks cause to enemies
	 */
	private int damageToEnemy;


	/**
	 * Initialization: sets the player's confidence to the max and sets initial values for damageByEnemy and damageToEnemy
	 */
	public GameControl()
	{
		confidence = maxConfidence;
		damageToPlayer = 5;
		damageToEnemy = 25;
	}

	/**
	 * Gets the amount of damage that enemies cause
	 */
	public int DamageToPlayer() { return damageToPlayer; }

	/**
	 * Adjusts the amount of damage enemies cause
	 */
	public void AdjustDamageToPlayerBy(int adjustment)
	{
		damageToPlayer += adjustment;

		if (damageToPlayer <= 0)
			Debug.LogWarning ("Enemies now do 0 damage or less");
	}

	/**
	 * Gets the amount of damage that the player causes
	 */
	public int DamageToEnemy() { return damageToEnemy; }

	/**
	 * Adjusts the amount of damage that the player causes
	 */
	public void AdjustDamageToEnemyBy(int adjustment)
	{
		damageToEnemy += adjustment;

		if (damageToEnemy <= 0)
			Debug.LogWarning ("The player now does 0 damage or less");
	}

	/**
	 * Adds to/removes from the confidence the player has (doesn't allow confidence to go over the maximum, and ends the game if it goes under 0)
	 */
	public void AdjustConfidenceBy(int confidenceChange)
	{
		confidence += confidenceChange;

		if (confidence > maxConfidence)
		{
			confidence = maxConfidence;
		}
		else if (confidence <= 0)
		{
			Load(); // The player has lost; return to the last save point
		}
	}

	/**
	 * Return's the player's confidence
	 */
	public int Confidence()
	{
		return confidence;
	}


	void CreateEmptyLevels(){
		for (int i = 0; i < levels.Length; i++) {
			levels [i] = new LevelData ();
			levels [i].bitBaddies = ~0;
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
			doorQuery = false;
			SceneManager.LoadScene(currentScene);

			confidence = data.confidence;
			playerPosition = data.playerPosition;
			if (PlayerPrefs.HasKey("Health"))
				confidence = PlayerPrefs.GetInt("Health");

			Debug.Log(data.playerPosition);

		}
		else
		{
			Debug.Log ("Load() was called, but there is no saved game to load");
		}
	}

	/**
	 * Load with making sure that control has been initialized
	 */
	public static void LoadNew()
	{
		if (control == null)
			control = new GameControl ();

		control.Load ();
	}
		
	/*
	 * Swaps Scenes, for use when entering doors between two overworld scenes
	 */
	public void SwapScene(int sceneToLoad,int doorToLoad){
		CacheLevelData ();
		door = doorToLoad;
		doorQuery = true;
		LoadWithFade(scenes [sceneToLoad]);

	}

	/* Allows calls from elsewhere to load a new scene */
	public static void LoadScene(string scene) {
		control.LoadWithFade(scene);
	}

	/* Loads a scene with a fading effect */
	private void LoadWithFade(string scene) {
		// Following block to create image on canvas largely was copied from:
		// https://answers.unity.com/questions/1034060/create-unity-ui-panel-via-script.html
		GameObject newCanvas = new GameObject("Canvas");
		Canvas imageCanvas = newCanvas.AddComponent<Canvas>();
		imageCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
		newCanvas.AddComponent<CanvasScaler>();
		newCanvas.AddComponent<GraphicRaycaster>();
		GameObject panel = new GameObject("Panel");
		panel.AddComponent<CanvasRenderer>();
		Image fadeImage = panel.AddComponent<Image>();
		panel.transform.SetParent(newCanvas.transform, false);
		// End copied //

		imageCanvas.scaleFactor = 10f;// A bit of a hack to make the image cover the screen
		DontDestroyOnLoad(imageCanvas);// keep canvas between scenes
		StartCoroutine(Fade(fadeImage, scene));
	}
	
	/* Coroutine to provide the fading effect */
	private IEnumerator Fade(Image fadeImage, string scene) {
		for (float f = 0; f < 1; f += Time.deltaTime) {
			fadeImage.color = new Color(0f, 0f, 0f, f);
			yield return null;
		}
		SceneManager.LoadScene(scene);
		for (float f = 1; f > 0; f -= Time.deltaTime) {
			fadeImage.color = new Color(0f, 0f, 0f, f);
			yield return null;
		}
		Destroy(fadeImage);// Manually destroy image/canvas
	}

	/*
	 * Enter an encounter scene, with the correct baddie, while keeping track of the level's state
	 */
	public void EnterEncounter (GameObject baddie){

		player.SetActive (false);

		baddieToDie = baddie.GetComponent<Baddie> ().GetIndex ();
		currentScene = SceneManager.GetActiveScene ().name;

		CacheLevelData ();

		try {
			EncounterControl.enemyPrefab = Resources.Load(baddie.tag) as GameObject;
		} catch (Exception e) {
			Debug.LogError("This enemy's type is not recognized: " + EncounterControl.enemyPrefab.tag);
			Debug.LogError("Error was: " + e);
		}

		LoadWithFade("encounter"); //loads scenes 
	}

	public void ExitEncounter (){
		doorQuery = false;
		levels [Array.IndexOf (scenes, currentScene)].bitBaddies &= (short)(~(1 << baddieToDie));
		SceneManager.LoadScene(currentScene);
		// @TODO: Would be nice to replace above line with below line,
		// but this function is called while EncounterControl runs and
		// the baddie is defeated, so this function gets called repeatedly
		//LoadWithFade(currentScene);
	}


	public Vector3 GetPlayerSpawn(){
		if (doorQuery) {
			return GameObject.Find ("SceneControl").GetComponent<SceneControl> ().doors[door].GetComponent<Door>().GetSpawnPoint();
		} else {
			if (playerPosition == null) {
				Debug.Log ("thats null?");
				return GameObject.Find ("SceneControl").GetComponent<SceneControl> ().neutralSpawnPoint;
			}
			return playerPosition.Value;
		}
	}

	public LevelData[] CacheLevelData(){
		playerPosition = player.transform.position;
		LevelData level = levels [Array.IndexOf (scenes, currentScene)];
		level.bitLamps = GameObject.Find ("SceneControl").GetComponent<SceneControl> ().GetLamps();
		level.bitBaddies = GameObject.Find ("SceneControl").GetComponent<SceneControl> ().GetBaddies();
		level.bitItems = GameObject.Find ("SceneControl").GetComponent<SceneControl> ().GetItems();
		return levels;
	}

	public LevelData GetLevelCache(){
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

/// <summary>
/// The data each individual "Level" or room needs
/// to save. EG. lamp info, badguy info, item info,
/// etc.
/// </summary>
[Serializable]
public class LevelData{
	public short bitLamps = 0;
	public short bitBaddies = 0;
	public short bitItems = 0;
}


[Serializable]
public class PlayerData{
	public int confidence = 100;
	public int maxConfidence = 100;
	public int defense = 5;
	public int attack = 35;
}