using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragCatchBox : MonoBehaviour {

	public Collider2D box;
	public bool onlyOne;
	public List<SnapDraggable> holding;
	public DragCatchBox overflow;
	public bool OneIngredientType;
	public Ingredient allowed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CatchMe(SnapDraggable drag)
	{
		Debug.Log("Catch Me");
		if (OneIngredientType)
		{
			Ingredient dragIng = drag.GetComponent<Ingredient>();

			if (dragIng != allowed)
			{
				drag.GoHome();
				return;
			}
		}

		if (!onlyOne || (holding.Count < 1))
		{
			holding.Add(drag);
			drag.gameObject.transform.position = gameObject.transform.position;
			drag.IHaveYou(this);
		} else if (overflow != null){
			overflow.CatchMe(drag);
		} else {
			drag.GoHome();
		}
	}

	public void ByeNow(SnapDraggable drag)
	{
		holding.Remove(drag);
	}
}
