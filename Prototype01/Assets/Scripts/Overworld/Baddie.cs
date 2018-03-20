using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Baddie : MonoBehaviour
{
    Rigidbody self;
    public Rigidbody boy;
    Vector3 target;
    public int speed;
    public int angle; 
    bool go; 

    // Use this for initialization
    void Start()
    {
        target = new Vector3(0, 0, 0);
        self = GetComponent<Rigidbody>();
        go = false; 
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            go = true; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            go = false; 
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene("sampleEncounter");
    }

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            target = boy.transform.position;

            target.y = 0;

            Vector3 veloc = (target - self.position);

            veloc = (veloc.normalized);
            veloc *= speed;

            self.velocity = veloc;

            if (veloc.magnitude > .1)
            {
                Vector3 lookto = new Vector3(target.x, self.position.y, target.z);
                self.transform.LookAt(lookto);
            }
        }
    }
}
