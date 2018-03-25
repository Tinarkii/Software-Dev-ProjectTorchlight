using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Baddie : MonoBehaviour {

    Rigidbody self; //The baddie we are refrencing/ maipulating

    public Rigidbody boy; //the movement target

    Vector3 target; //Vector towardsa a target 

    public int speed; //The movement speed 

    public int angle; //the point to loo at for y 

    bool go; //whether to move or not 

    // Use this for initialization of enemies 
    void Start(){
        target = new Vector3(0, 0, 0); //starts the target as none
        self = GetComponent<Rigidbody>(); //sets the varaibales for itself
        go = false; //no movement as of now
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
            go = true; 
    }

    /// <summary>
    /// OntriigreExit is called when this trigger area has been left
    /// by another rigidbody/collider.
    /// If the object that leaves the trigger is the player than 
    /// it removes the tag go for the baddie so he will know to stop moving
    /// in update
    void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player")
            go = false; 
    }

    /// <summary>
    /// OnCollisionEnter is called when this rigidbody has been 
    /// collided by another rigidbody/collider.
    /// If the object that enters the collision is the player than 
    /// it saves the game and loads the encounter scence 
    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Player"){
            GameControl.control.Save();
            SceneManager.LoadScene("sampleEncounter"); //loads scences 
        }
    }

    // Update is called once per frame
    void Update() {
        if (go) { //if we have the player in the range
            target = boy.transform.position; //the players postion 

            target.y = 0; //dont change the height 

            Vector3 veloc = (target - self.position); //the vectors magnitude and postion

            veloc = (veloc.normalized);
            veloc *= speed; //speed setter 

            self.velocity = veloc;

            if (veloc.magnitude > .1){
                Vector3 lookto = new Vector3(target.x, self.position.y + angle, target.z);
                self.transform.LookAt(lookto);
            }
        }
    }
}
