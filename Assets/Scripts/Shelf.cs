using UnityEngine;
using System.Collections;

public class Shelf : MonoBehaviour {

    public DragCatchBox[] level1;
    public DragCatchBox[] level2;
    public DragCatchBox[] level3;
    public DragCatchBox[] level4;
    int genpoint = 0;
    int totalboxes;

	// Use this for initialization
    void Start() 
    {
        totalboxes = level1.Length + level2.Length + level3.Length + level4.Length;
    }
	
	// Update is called once per frame
	void Update () {
        if (genpoint < totalboxes)
        {
            if (genpoint < level1.Length)
            {
                Ingredient ingr = IngredientGenerator.Instance.GenerateIngredient(2);
                ingr.GetComponent<SnapDraggable>().homeBox = level1[genpoint];
                ingr.GetComponent<SnapDraggable>().GoHome();
                genpoint++;
            }
        }
	}
}

