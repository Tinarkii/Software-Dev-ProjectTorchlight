using System.Collections.Generic;
using UnityEngine;
using System;

public class DefAction : MonoBehaviour {

	[Tooltip("A prefab of the player's shield that will be instantiated during encounters")]
	public Transform prefabForShieldOfPlayer;

	[Tooltip("A prefab of the player's blast that will be instantiated during encounters")]
	public Transform prefabForBlastOfPlayer;

	// The touches that are in progress, by unique id
	private Dictionary<int, Vector2> tracked;

	/* The various constants used */
	private static class Const {
		// The upward velocity added for a jump
		public const float jumpSpeed = 12.5f;
		// The constant that defines how many pixels a swipe is
		public const int Delta = 2;
	}

	private Rigidbody self;

	private bool canJump;


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

	protected void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "ShieldBad(Clone)" || col.gameObject.name == "BlastBad(Clone)") {
			Destroy(col.gameObject);
			// Player's health should go down here
		}
	}

	protected void OnCollisionStay(Collision col) {
		if (col.gameObject.name == "Cube" || col.gameObject.name == "BlockBad(Clone)")
			canJump = true;
	}

	protected void OnCollisionExit(Collision col) {
		if (col.gameObject.name == "Cube" || col.gameObject.name == "BlockBad(Clone)")
			canJump = false;
	}

	// # Begin region player actions. bool leftside refers to
	// whether the action happened on the left side or not.

	private void jump(bool leftside) {
		// Very hackish way to jump while preventing jumps in the air. Pls fix
		//if (self.velocity.y == 0)
		if (canJump)
			self.velocity = new Vector3(0, Const.jumpSpeed, 0);
	}

	/**
	 * Creates a ShieldOfPlayer near the player
	 * @param leftside Whether the shield should be made on the left side of the player
	 */
	private void shield(bool leftside) {
		int rev = leftside ? -1 : 1;
		Instantiate (prefabForShieldOfPlayer, this.transform.position + new Vector3(rev*1,0,0), Quaternion.Euler(0,rev*90,0));
	}

	private void shoot(bool leftside) {
		int rev = leftside ? -1 : 1;
		Instantiate (prefabForBlastOfPlayer, this.transform.position + new Vector3(rev*1,0.7f,0), Quaternion.Euler(0,rev*90,0));
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
		if (Input.GetKeyDown(KeyCode.RightArrow))
			shoot(false);
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			tap(false);

		if (Input.GetKeyDown(KeyCode.W))
			jump(true);
		if (Input.GetKeyDown(KeyCode.S))
			shield(true);
		if (Input.GetKeyDown(KeyCode.A))
			shoot(true);
		if (Input.GetKeyDown(KeyCode.D))
			tap(true);
	}
}
