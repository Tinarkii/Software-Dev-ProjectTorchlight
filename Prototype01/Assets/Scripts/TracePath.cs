using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TracePath : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("Entered");

	}
	public void OnPointerExit(PointerEventData eventData){
		Debug.Log ("Exited");

		//Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();
		//if (d != null) {
		//	d.transform.position = d.snapBack.position;
		//}
	}
}