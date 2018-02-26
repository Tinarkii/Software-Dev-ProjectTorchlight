using System.Collections.Generic;
using UnityEngine;
using System;

public class DefAction : MonoBehaviour {

	// The touches that are in progress, by unique id
	private Dictionary<int, Vector2> tracked;

	/* The various constants used */
	private static class Const {
		public const float jumpSpeed = 6;
		// The constant that defines how many pixels a swipe is
		public const int Delta = 2;
	}

	private Rigidbody self;


	// Initialization
	void Start () {
		self = GetComponent<Rigidbody>();
	}

	// Other initialization
	void OnEnable () {
		tracked = new Dictionary<int, Vector2>();
	}
	
	// Update is called once per frame
	void Update () {
		// For testing with keyboard inputs
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
				tracked.TryGetValue(touches[i].fingerId, out temp);// @TODO: Needs error handling
				
				// Calculates whether the touch happened on the left side of the screen
				bool side = touches[i].position.x < Screen.width/2;

				// Calculate the type of input
				if (Math.Abs(temp.x - touches[i].position.x) <= Const.Delta
							&& Math.Abs(temp.y - touches[i].position.y) <= Const.Delta) {
					tap(side);
				} else if (Math.Abs(temp.y - touches[i].position.y) > Math.Abs(temp.x - touches[i].position.x)) {
					if (temp.y - touches[i].position.y > 0)
						shield(side);
					else
						jump(side);
				} else {
					shoot(side);
				}

				tracked.Remove(touches[i].fingerId);
			}
		}
	}

	// # Begin region player actions. bool leftside refers to
	// whether the action happened on the left side or not.

	private void jump(bool leftside) {
		if (leftside)
			Debug.Log("Swipe up on left side");
		else
			Debug.Log("Swipe up on right side");
		self.velocity = new Vector3(0, Const.jumpSpeed, 0);
	}

	private void shield(bool leftside) {
		if (leftside)
			Debug.Log("Swipe down on left side");
		else
			Debug.Log("Swipe down on right side");
	}

	private void shoot(bool leftside) {
		if (leftside)
			Debug.Log("Swipe sideways on left side");
		else
			Debug.Log("Swipe sideways on right side");
	}

	private void tap(bool leftside) {
		if (leftside)
			Debug.Log("Tap on left side");
		else
			Debug.Log("Tap on right side");
	}

	// # End region player actions

	/* 
	 * Accepts keyboard inputs instead of touch inputs.
	 * Intended for testing without the need for a touchscreen.
	 */
	private void keyInput() {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		if (Input.GetKeyDown(KeyCode.UpArrow))
			jump(false);
		if (Input.GetKeyDown(KeyCode.DownArrow))
			shield(false);
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			shoot(false);
		if (Input.GetKeyDown(KeyCode.RightArrow))
			tap(false);

		if (Input.GetKeyDown(KeyCode.W))
			jump(true);
		if (Input.GetKeyDown(KeyCode.S))
			shield(true);
		if (Input.GetKeyDown(KeyCode.D))
			shoot(true);
		if (Input.GetKeyDown(KeyCode.A))
			tap(true);
	}
}
