using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShelfSpot : MonoBehaviour {

    DragCatchBox box;
    public Ingredient ingr;
    public Text nremainingtext;
    public int itemcount = 10;

	// Use this for initialization
	void Start () {
        box = GetComponent<DragCatchBox>();
        box.dragAdded += DragAdded;
        box.draggedIntoNew += DragInOtherBox;
	}

    public void InitBox(Ingredient ing)
    {
        box.allowed = ing;
        ingr = ing;
        if (box.holding.Count == 0)
        {
            Ingredient newingr = ((GameObject)Instantiate(ingr.gameObject)).GetComponent<Ingredient>();
            newingr.GetComponent<SnapDraggable>().homeBox = box;
            newingr.GetComponent<SnapDraggable>().GoHome();
            if (nremainingtext != null)
                nremainingtext.text = itemcount.ToString();
        }
    }

    void DragAdded(SnapDraggable drag)
    {
        if (box.holding.Count > 1)
        {
            box.holding.Remove(drag);
            Destroy(drag.gameObject);
            itemcount++;
            if (nremainingtext != null)
                nremainingtext.text = itemcount.ToString();
        }
    }

    void DragInOtherBox(SnapDraggable drag)
    {
        print("I'm getting called! " + this);
        print(box.draggedIntoNew);
        if (itemcount > 0)
        {
            Ingredient newingr = ((GameObject)Instantiate(ingr.gameObject)).GetComponent<Ingredient>();
            newingr.GetComponent<SnapDraggable>().homeBox = box;
            newingr.GetComponent<SnapDraggable>().GoHome();
            itemcount--;
            if (nremainingtext != null)
                nremainingtext.text = itemcount.ToString();
        }
    }
}
