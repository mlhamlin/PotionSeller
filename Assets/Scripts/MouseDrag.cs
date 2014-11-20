using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//	void OnMouseDown() {
//	}
//	void OnMouseUp() {
//	}
	void OnMouseDrag() {
		Vector2 cursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		gameObject.transform.position = cursorPosition;
	}
}
