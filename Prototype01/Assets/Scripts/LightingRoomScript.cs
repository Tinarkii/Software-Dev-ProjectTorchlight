using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingRoomScript : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{

        if(Mathf.Abs(Input.mousePosition.x - Screen.width/2) > (Screen.width / 5))
        {
            transform.
            transform.Translate(Mathf.Sign(Input.mousePosition.x - Screen.width / 2) * -0.08f,0,0,Space.World);
        }
		
	}
}
