using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSave : MonoBehaviour {
	public GameObject dialogueBoxUIObject;
	void Start()
	{
		dialogueBoxUIObject.SetActive(false);
	}

	void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.tag == "Player")
		{
			Time.timeScale = 0;
			dialogueBoxUIObject.SetActive(true);
		}    
    }

	public void OnYesClick()
	{
		dialogueBoxUIObject.SetActive(false);
		Time.timeScale = 1;
		GameControl.control.Save();
	}
	public void OnNoClick()
	{
		dialogueBoxUIObject.SetActive(false);
		Time.timeScale = 1;
	}

}
