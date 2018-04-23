using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Baddie : MonoBehaviour {

    Rigidbody self; //The baddie we are refrencing/ maipulating

	private Rigidbody boy; //the movement target

    Vector3 target; //Vector towardsa a target 

    public int speed; //The movement speed 

    public int angle; //the point to loo at for y 

    bool go = false; //whether to move or not starts out not moving  

	private int index = -1; //position in array of baddie

	public bool alive = true; //if the baddy is alive

    private Animator anim = null; //sets what animation to be active


    // Use this for initialization of enemies 
	void Start(){
		boy = GameControl.control.GetPlayer ().GetComponent<Rigidbody>(); //grabs the boy from GameControl.control
        anim = gameObject.GetComponent<Animator>(); //sets what gameObect anim should control
        target = new Vector3(0, 0, 0); //starts the target as none
        self = GetComponent<Rigidbody>(); //sets the varaibales for itself
    }

    /// <summary>
    /// OnTriggerEnter is called when this trigger area has been entered
    /// by another rigidbody/collider.
    /// If the object that enters the trigger is the player than 
    /// it sets the tag go for the baddie so he will know to move towards the player
    /// in update
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player")
         {
            go = true; 
         }   
    }

    /// <summary>
    /// OnTriggerExit is called when this trigger area has been left
    /// by another rigidbody/collider.
    /// If the object that leaves the trigger is the player than 
    /// it removes the tag go for the baddie so he will know to stop moving
    /// in update
    void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player")
        {
            go = false;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this rigidbody has been 
    /// collided by another rigidbody/collider.
    /// If the object that enters the collision is the player than 
    /// it saves the game and loads the encounter scence 
    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Player")
			GameControl.control.EnterEncounter (gameObject);
    }

	public void SetIndex(int i){
		index = i;
	}
	public int GetIndex(){
		return index;
	}

    // Update is called once per frame
    void FixedUpdate() {

		if (!alive) { //if the player has been defeated 
			Destroy (gameObject); //destroy the enemy 
		}

        if (go) { //if we have the player in the range
            target = boy.transform.position; //the players postion 

            target.y = 0; //dont change the height 

            Vector3 veloc = (target - self.position); //the vectors magnitude and postion

            veloc = (veloc.normalized); //normalize the vector 
            veloc *= speed; //speed setter 

            self.velocity = veloc; // set his velocity to the speed and direction 

            if (veloc.magnitude > .1){ //if the vector is greater than .1
                anim.SetTrigger("Walking");
                Vector3 lookto = new Vector3(target.x, self.position.y + angle, target.z); //look at the player
                self.transform.LookAt(lookto); //turn to look at the boy 
            }
        }
        else 
            anim.SetTrigger("Standing");
    }
}
