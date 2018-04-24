using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSave : MonoBehaviour {
	public GameObject dialogueBoxUIObject;
	void Start()
	{
		dialogueBoxUIObject = GameObject.FindGameObjectWithTag("dialogueBoxUIObject");
		dialogueBoxUIObject.SetActive(false);
	}
	void OnMouseDown()
	{
		dialogueBoxUIObject.SetActive(true);
		Time.timeScale = 0;
	}
	public void OnYesClick()
	{
		Time.timeScale = 1;
		GameControl.control.Save();
	}
	public void OnNoClick()
	{
		Time.timeScale = 1;
		dialogueBoxUIObject.SetActive(false);
	}

}
