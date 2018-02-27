using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform ParentToReturnTo = null;
	public enum Slot {WEAPON, ARMOR, SPELL, Inventory};
	public Slot typeOfItem = Slot.WEAPON;

	public void OnBeginDrag(PointerEventData eventData){
		//Debug.Log ("OnBeginDrag");
		ParentToReturnTo = this.transform.parent;
		this.transform.SetParent (this.transform.parent.parent);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
	public void OnDrag(PointerEventData eventData){
		//Debug.Log ("OnDrag");
		this.transform.position = eventData.position;
	}
	public void OnEndDrag(PointerEventData eventData){
		//Debug.Log ("OnEndDrag");
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		this.transform.SetParent (ParentToReturnTo);
	}
}
