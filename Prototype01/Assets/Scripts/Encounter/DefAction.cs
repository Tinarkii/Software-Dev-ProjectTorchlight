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

	private Animator anim;
	public GameObject boyo;
	

	/* The various constants used */
	private static class Const {
		// The upward velocity added for a jump
		public const float jumpSpeed = 6f;
		// The player's gravity (affects fall speed)
		public const float gravity = 0.2f;
		// The constant that defines how many pixels a swipe is (versus a tap). Currently 0; non-functional
		public const int Delta = 0;
		// How long the delay is before the player can repeat a move
		public const float delayTime = 1f;
	}

	// A reference to the player's rigidbody for movement
	private Rigidbody self;

	// Whether the player is in a state to allow jumping
	private bool canJump;

	// The amount of confidence the player loses per hit
	private int hitAmount;
		
	// The delay until the player can perform a certain action again
	private float shieldDelayLeft = 0;
	private float shieldDelayRight = 0;
	private float shootDelayLeft = 0;
	private float shootDelayRight = 0;


	// Initialization
	void Start () {
		anim = boyo.GetComponent<Animator>();
		confidenceText.text = "Player's Confidence: " + GameControl.control.Confidence();
		self = GetComponent<Rigidbody>();
		hitAmount = -5;
	}

	/* Reset tracked finger inputs */
	// Other initialization
	void OnEnable () {
		tracked = new Dictionary<int, Vector2>();
	}

	// Simulates a weaker gravity to increase jump time
	void FixedUpdate() {
		self.velocity = new Vector3(0, self.velocity.y - Const.gravity, 0);
	}
	
	/* Take player input, decrease delays */
	// Update is called once per frame
	void Update () {
		// Update delays
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
			GameControl.control.AdjustConfidenceBy(hitAmount);
			confidenceText.text = "Player's Confidence: " + GameControl.control.Confidence();

			Destroy(col.gameObject);
		}
	}

	/* Handles the player's interactions with blocks */
	protected void OnTriggerStay(Collider col) {
		// Only concerned with block interactions
		if (col.gameObject.name != "BlockBad(Clone)")
			return;
		
		
		if (canHit == 0) {// Only cause damage once
			GameControl.control.AdjustConfidenceBy(hitAmount);
			confidenceText.text = "Player's Confidence: " + GameControl.control.Confidence();

			// Push player on top of block so he doesn't get stuck
			// @TODO: Should really round down before adding
			float pos = self.position.y + (6.5f/4);// Fix magic numbers; also in Defense.cs
			self.position = new Vector3(self.position.x, pos, self.position.z);
		}
		canHit--;
	}

	// So that the player only takes damage after the 1st frame. A bit hackish.
	private int canHit = 1;
	/* Player can take damage from another block after contact from the first has ended */
	protected void OnTriggerExit(Collider col) {
		if (col.gameObject.name == "BlockBad(Clone)")
			canHit = 1;
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
			shieldDelayLeft = Const.delayTime;
		} else {
			if (shieldDelayRight > 0)
				return;
			shieldDelayRight = Const.delayTime;
		}

		int rev = leftside ? -1 : 1;
		Instantiate (prefabForShieldOfPlayer, this.transform.position + new Vector3(rev*1,0,0), Quaternion.Euler(0,rev*90,0));
	}

	/* Create blast object */
	private void shoot(bool leftside) {
		if (leftside) {
			if (shootDelayLeft > 0)
				return;
			shootDelayLeft = Const.delayTime;
		} else {
			if (shootDelayRight > 0)
				return;
			shootDelayRight = Const.delayTime;
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
		{
			anim.SetTrigger("Jump");
			StartCoroutine(Wait());
			
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			anim.SetTrigger("Shield");
			shield(false);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			anim.SetTrigger("Fire");
			shoot(false);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			tap(false);

		if (Input.GetKeyDown(KeyCode.W))
		{
			anim.SetTrigger("Jump");
			StartCoroutine(Wait());
			//jump(true);
		}
		if (Input.GetKeyDown(KeyCode.S))
			shield(true);
		if (Input.GetKeyDown(KeyCode.A))
			shoot(true);
		if (Input.GetKeyDown(KeyCode.D))
			tap(true);
	}
	private IEnumerator<WaitForSeconds> Wait()
	{
		Debug.Log("Waiting");
    	yield return new WaitForSeconds(0.1f);
		jump(false);
		Debug.Log("Done Waiting");

  	}
	
}
