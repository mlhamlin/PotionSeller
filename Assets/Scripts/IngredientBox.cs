using UnityEngine;
using System.Collections;

public class IngredientBox : MonoBehaviour {

	public int count; //how many of this ingredient I have
	public Ingredient templateIngredient;
	public Ingredient grabable;
    public DragCatchBox box;


	// Use this for initialization
	void Start () {
        box.dragAdded += (SnapDraggable s) =>
        {
            if (box.holding.Count > 0)
            {
                
            }
            count += 1;
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
