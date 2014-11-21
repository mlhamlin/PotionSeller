using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Draggable : MonoBehaviour {
	List<Collider2D> colliders = new List<Collider2D>();

	public void OnMouseDrag() {
		Vector2 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		gameObject.transform.position = cursorPosition;
	}
	public void OnTriggerEnter2D(Collider2D other) {
		colliders.Add(other);
		Debug.Log("Entering: " + colliders.Count + " colliders");
	}
	public void OnTriggerExit2D(Collider2D other) {
		colliders.Remove(other);
		Debug.Log("Exiting: " + colliders.Count + " colliders");
	}
}