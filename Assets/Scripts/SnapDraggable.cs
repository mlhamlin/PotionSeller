﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnapDraggable : Draggable {
	
	List<DragCatchBox> boxes = new List<DragCatchBox>();
	public DragCatchBox homeBox;
	public DragCatchBox current;
		
	public void OnTriggerEnter2D(Collider2D other) {
		DragCatchBox box = other.GetComponent<DragCatchBox>();
		if (box != null)
		{
			boxes.Add(box);
		}
	}

	public void OnTriggerExit2D(Collider2D other) {
		DragCatchBox box = other.GetComponent<DragCatchBox>();
		if (box != null)
		{
			boxes.Remove(box);
		}		
	}
		
	public void OnMouseDrag() {
		Vector2 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		gameObject.transform.position = cursorPosition;
		if (current != null)
		{
			current.ByeNow(this);
			current = null;
		}
	}
		
	public void OnMouseUp() {

		if (boxes.Count > 0) {
			Vector2 pos = gameObject.transform.position;
			Vector2 BoxPos = boxes[0].transform.position;
			DragCatchBox fave = boxes[0];
			//manhattan distance
			float dist = Mathf.Abs(pos.x - BoxPos.x) + Mathf.Abs(pos.y - BoxPos.y);
			for (int i = 1; i < boxes.Count; i += 1) {
				Vector2 BoxPos2 = boxes[i].transform.position;
				float dist2 = Mathf.Abs(pos.x - BoxPos2.x) + Mathf.Abs(pos.y - BoxPos2.y);
				if (dist2 < dist) {
					dist = dist2;
					BoxPos = BoxPos2;
					fave = boxes[i];
				}
			}

			fave.CatchMe(this);

		} else {
			GoHome();
		}
	}

	//Throws draggable back to home box
	public void GoHome() {
		homeBox.CatchMe(this);
	}

	//Allows DragCatchBox to inform SnapDraggable which box ended up with it
	public void IHaveYou(DragCatchBox box)
	{
		current = box;
	}
}