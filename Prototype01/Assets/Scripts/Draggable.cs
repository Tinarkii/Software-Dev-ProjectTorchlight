using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform ParentToReturnTo = null;
	public Transform snapBack = null;

	public void OnBeginDrag(PointerEventData eventData){
		//Debug.Log ("OnBeginDrag");

		snapBack = this.transform.parent;
		ParentToReturnTo = this.transform.parent;
		this.transform.SetParent (this.transform.parent.parent);
	}
	public void OnDrag(PointerEventData eventData){
		//Debug.Log ("OnDrag");

		this.transform.position = eventData.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	public void OnEndDrag(PointerEventData eventData){
		//Debug.Log ("OnEndDrag");
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		this.transform.SetParent (ParentToReturnTo);
	}
}
