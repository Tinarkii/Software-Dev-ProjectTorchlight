using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class OverWorldNavOG : MonoBehaviour {
    Rigidbody self;
    Vector3 target;
    public int maxSpeed;
    RaycastHit hitPoint;
	private Camera usedCamera;
	private Vector3 veloc;
	private int bufferFrame = 0;
	private Animator anim;
	public GameObject boy;
    private float p;
    private float i;
    private float integral;
	[Tooltip("The minimum speed for which the boy has a walking animation")]
	public float minSpeedForWalkingAnimation;

    // Use this for initialization
    void Start () {
		usedCamera = Camera.main;
		target = transform.position;
		veloc = new Vector3 (0, 0, 0);
        self = GetComponent<Rigidbody>();
		anim = boy.GetComponent<Animator>();
        p = 1.75f;//original value 6.5f / 7.0f;
        i = 0.3f;
        integral = 0f;
    }

	public void Cleanse(){
		target = this.transform.position;
		bufferFrame = 1;
	}
	
    /**
     * Calculate the P (proportional) value for movement
     */
    private Vector3 calcP(Vector3 target, Vector3 current)
    {
        Vector3 error = target - current;

        Vector3 pOut = error * p;

        Vector3 toReturn = pOut;
        return toReturn;
    }
    /**
     * Calculate the I (integral) value for movement
     */
    private float calcI(Vector3 target, Vector3 current)
    {
        float iOut = 0;
        integral += Mathf.Abs(target.magnitude - current.magnitude) / 5f;
        iOut = i * integral;
        return iOut;
    }

	// Update is called once per frame
	void Update ()
    // NOTE: I changed this from FixedUpdate to Update to make UI detection
    // work properly. If we need it was FixedUpdate (say, for phsyics reasons)
    // the functionality should be split up into two methods
    {

		usedCamera = Camera.main;
		if (usedCamera == null) {
			Debug.LogWarning ("Can't Find Camera");
			return;
		}

        // Do not take new raycast position if UI element selected
        // Touch input
        bool inputUI = false;
        foreach (Touch touch in Input.touches) {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
                inputUI = true;
        }
        // Mouse input
        if (EventSystem.current.IsPointerOverGameObject())
            inputUI = true;

        Ray ray = usedCamera.ViewportPointToRay(usedCamera.ScreenToViewportPoint(Input.mousePosition));
        if (Input.GetMouseButton(0) && !inputUI)
        {
			if (Physics.Raycast (ray, out hitPoint, 1000, (1 << 9)) && bufferFrame < 1) {
                target = hitPoint.point;
            } else {
				bufferFrame--;
			}
        }

        veloc = calcP(target, self.position);

        float speed = Mathf.Min (Mathf.Abs(maxSpeed), Mathf.Abs(veloc.magnitude) + calcI(target, self.position));
           
		veloc = (veloc.normalized);
           
		veloc = Vector3.Scale (veloc, new Vector3 (speed, 0, speed));

		self.velocity = new Vector3 (veloc.x, self.velocity.y, veloc.z);
		if (veloc.magnitude > .1) {
			Vector3 lookto = new Vector3 (target.x, self.position.y, target.z);
			self.transform.LookAt (lookto);
        } else {
			target = transform.position;
			self.velocity = new Vector3(0,0,0);
		}

        if (!Input.GetMouseButton(0))
        {
            if (Mathf.Abs(target.magnitude - self.position.magnitude) <= .25f && veloc.magnitude > .5f)
            {
                self.velocity = new Vector3(0, 0, 0);
                speed = 0f;
                integral = 0;
            }
        }
        if (Mathf.Abs((target - self.position).x) > 0.2f && Mathf.Abs((target - self.position).z) > 0.2f && speed > 0f)
        {
            //anim.SetTrigger("ImprovedWalking");
            anim.SetTrigger("ImprovedWalking");
        }
        else
        {
            //Debug.Log((target - self.position).x + ", " + (target - self.position).y + ", " + (target - self.position).z);
            anim.SetTrigger("Standing");
        }
    }
}
