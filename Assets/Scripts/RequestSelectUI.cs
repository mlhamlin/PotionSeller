using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RequestSelectUI : MonoBehaviour {

	public Canvas SelectWindow;
	private bool windowOpen;

	public bool frozen
	{
		get { return windowOpen; }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenWindow()
	{
		windowOpen = true;
		SelectWindow.enabled = true;
	}

	public void CloseWindow()
	{
		windowOpen = false;
		SelectWindow.enabled = false;
	}
}
