using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class DisplayInfo : MonoBehaviour {
 
    public string myString;
    public Text myText;
    public float fadeTime;
    public bool displayInfo;
 
    // Use this for initialization
    void Start () {
		if (gameObject.name.Contains("ABC"))
        	myText = GameObject.Find ("ABC").GetComponent<Text> ();
		else if (gameObject.name.Contains("Rub"))
        	myText = GameObject.Find ("Rub").GetComponent<Text> ();
		else if (gameObject.name.CompareTo("Fruit Snacks") == 0)
        	myText = GameObject.Find ("Fruit").GetComponent<Text> ();
		else if (gameObject.name.CompareTo("Fruit Snacks (1)") == 0)
        	myText = GameObject.Find ("Fruitsnek").GetComponent<Text> ();
		
		//myText.color = Color.white;
        myText.color = Color.clear;
    }
     
    // Update is called once per frame
    void Update () 
    {
    	Text ();
    }
 
    void OnMouseOver()
    {
        displayInfo = true;
    }

    void OnMouseExit()
    {
        displayInfo = false;
    }
 
    void Text ()
    {
        if(displayInfo)
        {
			myText.text = myString;
			myText.color = Color.white;
        }
        else
        {     
            myText.color = Color.clear;
        }
    }
 
 
 
}