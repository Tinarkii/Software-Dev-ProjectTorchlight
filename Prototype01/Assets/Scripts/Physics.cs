using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {
    Rigidbody self;
    Vector3 target;
    public int power;

	// Use this for initialization
	void Start () {
        target = new Vector3(0, 0, 0);
        self = GetComponent<Rigidbody>();
		//tracked = new Dictionary<int, Vector3>();
	}
	
	// Update is called once per frame
	void Update () {

		/*Touch[] touches = Input.touches;
		for (int i = 0; i < Input.touchCount; i++) {
			if (touches [i].phase == TouchPhase.Began) {
				Vector2 temp = new Vector2 () { x = touches [i].position.x, y = touches [i].position.y };
				tracked.Add (touches [i].fingerId, temp);
			}

			if (touches [i].phase == TouchPhase.Canceled) {
				tracked.Remove (touches [i].fingerId);
			}

			if (touches [i].phase == TouchPhase.Ended) {
				Vector2 temp;
				tracked.TryGetValue (touches [i].fingerId, out temp);//Needs error handling
			}
		}*/

        if (Input.GetMouseButton(0))
        {
			target = new  
        }
        else
        {
            target = new Vector3(0, 0, 0);
        }

		target.Normalize();
		target.Scale(new Vector3(power, power, power));

		self.velocity = new Vector3(-target.x, self.velocity.y, target.z); 

        //self.AddRelativeForce(target);

        //Debug.Log(Input.mousePosition.ToString());


    }
}
