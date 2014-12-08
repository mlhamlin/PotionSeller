using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DragCatchBox : MonoBehaviour {

	public Collider2D box;
	public bool onlyOne;
	public List<SnapDraggable> holding;
	public DragCatchBox overflow;
	public bool OneIngredientType;
	public Ingredient allowed;

	public delegate void DraggableAdded(SnapDraggable drag);
	public DraggableAdded dragAdded;
	public delegate void DraggableRemoved(SnapDraggable drag);
	public DraggableRemoved dragRemoved;
    public delegate void DraggableInNew(SnapDraggable drag);
    public DraggableInNew draggedIntoNew;

	public Text count;

	// Use this for initialization
	void Start () {
		updateCountUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Item is attempting to enter the box
	public void CatchMe(SnapDraggable drag)
	{
		if (OneIngredientType)
		{
			Ingredient dragIng = drag.GetComponent<Ingredient>();

			if (!dragIng.Equals(allowed))
			{
				drag.GoHome();
				return;
			}
		}

		if (!onlyOne || (holding.Count < 1))
		{
			ItsTrueLove(drag);
		} else if (overflow != null){
			overflow.CatchMe(drag);
		} else {
			drag.GoHome();
		}
	}

	//Item is leaving the box
	public void ByeNow(SnapDraggable drag)
	{
		holding.Remove(drag);
		if (dragRemoved != null)
		{
			dragRemoved(drag);
		}
		updateCountUI();
	}

    public void ReallyBye(SnapDraggable drag)
    {
        if (draggedIntoNew != null)
        {
            draggedIntoNew(drag);
        }
    }

	//Item is really being added to this box
	public void ItsTrueLove(SnapDraggable drag)
	{
		holding.Add(drag);
		Vector3 goHere = gameObject.transform.position;
		goHere.z = SnapDraggable.STATIONARY_Z;
		drag.gameObject.transform.position = goHere;
		drag.IHaveYou(this);
		if (dragAdded != null)
		{
			dragAdded(drag);
		}
		updateCountUI();
	}

	public void updateCountUI()
	{
		if (count != null)
		{
			count.text = holding.Count.ToString();
		}
	}

	public void ForgetItAll()
	{
		holding.Clear();
	}
}
