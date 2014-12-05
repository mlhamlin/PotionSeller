using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnapDraggable : MonoBehaviour {
	
	List<DragCatchBox> boxes = new List<DragCatchBox>();
	public DragCatchBox homeBox;
	public DragCatchBox current;
	public const int DRAGGING_Z = -2;
	public const int STATIONARY_Z = -1;
		
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
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		cursorPosition.z = DRAGGING_Z;
		gameObject.transform.position = cursorPosition;
		if (current != null)
		{
			current.ByeNow(this);
		}
	}
		
	public void OnMouseUp() {

		if (boxes.Count > 0) {
			Vector3 pos = gameObject.transform.position;
			Vector3 BoxPos = boxes[0].transform.position;
			DragCatchBox fave = boxes[0];
			//manhattan distance
			float dist = Mathf.Abs(pos.x - BoxPos.x) + Mathf.Abs(pos.y - BoxPos.y);
			for (int i = 1; i < boxes.Count; i += 1) {
				Vector3 BoxPos2 = boxes[i].transform.position;
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
        current = null;
		homeBox.CatchMe(this);
	}

	//Allows DragCatchBox to inform SnapDraggable which box ended up with it
	public void IHaveYou(DragCatchBox box)
	{
        if (current != null)
            current.ReallyBye(this);
		current = box;
	}
}
