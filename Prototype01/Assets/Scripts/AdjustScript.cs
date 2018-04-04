using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AdjustScript : MonoBehaviour {

	void OnGUI()
	{
			if (GUI.Button(new Rect(10,100,100,30),"Health Up"))
			{
				GameControl.control.AdjustConfidenceBy(10);
			}
			if (GUI.Button(new Rect(10,140,100,30),"Health Down"))
			{
				GameControl.control.AdjustConfidenceBy(-10);
			}
			if (GUI.Button(new Rect(10,180,100,30),"Save"))
			{
				GameControl.control.Save();
			}
			if (GUI.Button(new Rect(10,220,100,30),"Load"))
			{
				GameControl.control.Load();
			}	
	}

}
