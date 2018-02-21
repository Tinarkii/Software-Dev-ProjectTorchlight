using System.Collections.Generic;
using UnityEngine;
using System;

public class ControlTouchText : MonoBehaviour {

	// The touches that are in progress, by unique id
	private Dictionary<int, Vector2> tracked;

	// The constant that defines how many pixels a swipe is
	private static class Const { public const int Delta = 2; }


	// Initialization
	void Start () {
		tracked = new Dictionary<int, Vector2>();
	}
	
	// Update is called once per frame
	void Update () {
		// For testing without touch inputs
		keyInput();
		
		Touch[] touches = Input.touches;
		for(int i = 0; i < Input.touchCount; i++) {

			if (touches[i].phase == TouchPhase.Began){
				Vector2 temp = new Vector2() { x = touches[i].position.x, y = touches[i].position.y };
				tracked.Add(touches[i].fingerId, temp);
			}

			if (touches[i].phase == TouchPhase.Canceled){
				tracked.Remove(touches[i].fingerId);
			}

			if (touches[i].phase == TouchPhase.Ended){
				Vector2 temp;
				tracked.TryGetValue(touches[i].fingerId, out temp);//Needs error handling
				
				String action;
				// Calculate the type of input
				if (Math.Abs(temp.x - touches[i].position.x) <= Const.Delta
							&& Math.Abs(temp.y - touches[i].position.y) <= Const.Delta) {
					action = "Tap ";
				} else if (Math.Abs(temp.y - touches[i].position.y) > Math.Abs(temp.x - touches[i].position.x)) {
					if (temp.y - touches[i].position.y > 0)
						action = "Swipe down ";
					else
						action = "Swipe down ";
				} else {
					action = "Swipe sideways ";
				}

				// Defines on which side of the screen the input took place
				if (touches[i].position.x < Screen.width/2)
					Debug.Log(action + "on left side (touch)");
				else
					Debug.Log(action + "on right side (touch)");

				tracked.Remove(touches[i].fingerId);
			}
		}
	}

	/* 
	 * Accepts keyboard inputs instead of touch inputs,
	 * intended for testing without the need for a touchscreen
	 */
	private void keyInput() {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		if (Input.GetKeyDown(KeyCode.UpArrow))
			Debug.Log("Swipe up on right side (keyboard)");
		if (Input.GetKeyDown(KeyCode.DownArrow))
			Debug.Log("Swipe down on right side (keyboard)");
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			Debug.Log("Swipe sideways on right side (keyboard)");
		if (Input.GetKeyDown(KeyCode.RightArrow))
			Debug.Log("Tap on right side (keyboard)");

		if (Input.GetKeyDown(KeyCode.W))
			Debug.Log("Swipe up on left side (keyboard)");
		if (Input.GetKeyDown(KeyCode.S))
			Debug.Log("Swipe down on left side (keyboard)");
		if (Input.GetKeyDown(KeyCode.D))
			Debug.Log("Swipe sideways on left side (keyboard)");
		if (Input.GetKeyDown(KeyCode.A))
			Debug.Log("Tap on left side (keyboard)");
	}
}
