using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DefAction : MonoBehaviour {

	[Tooltip("A prefab of the player's shield that will be instantiated during encounters")]
	public Transform prefabForShieldOfPlayer;

	[Tooltip("A prefab of the player's blast that will be instantiated during encounters")]
	public Transform prefabForBlastOfPlayer;

	// The Text element that displays the player's confidence
	public Text confidenceText;

	// The touches that are in progress, by unique id
	private Dictionary<int, Vector2> tracked;

	/* The various constants used */
	private static class Const {
		// The upward velocity added for a jump
		public const float jumpSpeed = 12.5f;
		// The constant that defines how many pixels a swipe is (versus a tap)
		public const int Delta = 0;
	}

	// A reference to the player's rigidbody for movement
	private Rigidbody self;

	// Whether the player is in a state to allow jumping
	private bool canJump;
		
	// The delay until the player can perform a certain action again
	private float shieldDelayLeft = 0;
	private float shieldDelayRight = 0;
	private float shootDelayLeft = 0;
	private float shootDelayRight = 0;


	// Initialization
	void Start () {
		confidenceText.text = "Player's Confidence: " + GameControl.control.confidence.ToString();
		self = GetComponent<Rigidbody>();
	}

	/* Reset tracked finger inputs */
	// Other initialization
	void OnEnable () {
		tracked = new Dictionary<int, Vector2>();
	}
	
	/* Take player input, decrease delays */
	// Update is called once per frame
	void Update () {
		if (shieldDelayLeft > 0)
			shieldDelayLeft -= Time.deltaTime;
		if (shieldDelayRight > 0)
			shieldDelayRight -= Time.deltaTime;
		if (shootDelayLeft > 0)
			shootDelayLeft -= Time.deltaTime;
		if (shootDelayRight > 0)
			shootDelayRight -= Time.deltaTime;

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

	/* If in contact with a solid, allow jumping */
	protected void OnCollisionStay(Collision col) {
		if (col.gameObject.name == "Cube" || col.gameObject.name == "BlockBad(Clone)")
			canJump = true;
	}

	/* If not in contact with a solid, do not allow jumping */
	protected void OnCollisionExit(Collision col) {
		if (col.gameObject.name == "Cube" || col.gameObject.name == "BlockBad(Clone)")
			canJump = false;
	}

	/* Handles when the player is hit with a enemy blast or shield */
	protected void OnTriggerEnter(Collider col) {
		if (col.gameObject.name == "ShieldBad(Clone)" || col.gameObject.name == "BlastBad(Clone)") {
			Destroy(col.gameObject);
			GameControl.control.confidence -= 15;
			if (GameControl.control.confidence < 0)
				GameControl.control.confidence = 0;
			confidenceText.text = "Player's Confidence: " + GameControl.control.confidence.ToString();
		}
	}

	// # Begin region player actions. @param bool leftside refers to
	// whether the action happened on the left side or not.

	/* Jump if allowed */
	private void jump(bool leftside) {
		if (canJump)
			self.velocity = new Vector3(0, Const.jumpSpeed, 0);
	}

	/**
	 * Creates a ShieldOfPlayer near the player
	 * @param leftside Whether the shield should be made on the left side of the player
	 */
	private void shield(bool leftside) {
		if (leftside) {
			if (shieldDelayLeft > 0)
				return;
			shieldDelayLeft = 0.3f;
		} else {
			if (shieldDelayRight > 0)
				return;
			shieldDelayRight = 0.3f;
		}

		int rev = leftside ? -1 : 1;
		Instantiate (prefabForShieldOfPlayer, this.transform.position + new Vector3(rev*1,0,0), Quaternion.Euler(0,rev*90,0));
	}

	/* Create blast object */
	private void shoot(bool leftside) {
		if (leftside) {
			if (shootDelayLeft > 0)
				return;
			shootDelayLeft = 0.3f;
		} else {
			if (shootDelayRight > 0)
				return;
			shootDelayRight = 0.3f;
		}

		int rev = leftside ? -1 : 1;
		Instantiate (prefabForBlastOfPlayer, this.transform.position + new Vector3(rev*1,0.7f,0), Quaternion.Euler(0,rev*90,0));
	}

	/* The user did a tap rather than a swipe */
	private void tap(bool leftside) {
		// Currently unimplemented. May not ever be.
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
