using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShelfSpot : MonoBehaviour {

    DragCatchBox box;
    public Ingredient ingr;
    public Text nremainingtext;
    public SpriteRenderer fakeingr;
    public int itemcount = 3;

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
		ing.gameObject.transform.parent = gameObject.transform;
        fakeingr.sprite = ingr.GetComponent<SpriteRenderer>().sprite;
        fakeingr.enabled = false;
        fakeingr.color = new Color(ingr.color.r/2, ingr.color.g/2, ingr.color.b/2);
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
            fakeingr.enabled = false;
        }
    }

    void DragInOtherBox(SnapDraggable drag)
    {
        print("I'm getting called! " + this);
        print(box.draggedIntoNew);
        itemcount--;
        if (itemcount > 0)
        {
            Ingredient newingr = ((GameObject)Instantiate(ingr.gameObject)).GetComponent<Ingredient>();
            newingr.GetComponent<SnapDraggable>().homeBox = box;
            newingr.GetComponent<SnapDraggable>().GoHome();
        }
        else 
            fakeingr.enabled = true;
        if (nremainingtext != null)
            nremainingtext.text = itemcount.ToString();
    }
}
