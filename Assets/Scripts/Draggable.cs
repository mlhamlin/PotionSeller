using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Draggable : MonoBehaviour {
	private List<Collider2D> colliders = new List<Collider2D>();

	public void OnTriggerEnter2D(Collider2D other) {
		colliders.Add(other);
//		Debug.Log("Entering: " + colliders.Count + " colliders");
	}
	public void OnTriggerExit2D(Collider2D other) {
		colliders.Remove(other);
//		Debug.Log("Exiting: " + colliders.Count + " colliders");
	}

	public void OnMouseDrag() {
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		gameObject.transform.position = cursorPosition;
	}
	public void OnMouseUp() {
		if (colliders.Count > 0) {
			Vector3 pos = gameObject.transform.position;
			Vector3 colliderPos = colliders[0].transform.position;
			//manhattan distance
			float dist = Mathf.Abs(pos.x - colliderPos.x) + Mathf.Abs(pos.y - colliderPos.y);
			for (int i = 1; i < colliders.Count; i += 1) {
				Vector3 colliderPos2 = colliders[i].transform.position;
				float dist2 = Mathf.Abs(pos.x - colliderPos2.x) + Mathf.Abs(pos.y - colliderPos2.y);
				if (dist2 < dist) {
					dist = dist2;
					colliderPos = colliderPos2;
				}
			}
			gameObject.transform.position = colliderPos;
		}
	}
}