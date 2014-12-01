using UnityEngine;
using System.Collections;

public class Shelf : UnitySingleton<Shelf> {

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
            DragCatchBox box = level1[0];
            int ingramt = 0;
            if (genpoint < level1.Length)
            {
                ingramt = 2;
                box = level1[genpoint];
            }
            else if (genpoint < 12 + level2.Length)
            {
                ingramt = 4;
                box = level2[genpoint - 12];
            }
            else if (genpoint < 20 + level3.Length)
            {
                ingramt = 6;
                box = level3[genpoint - 20];
            }
            else if (genpoint < 28 + level4.Length)
            {
                ingramt = 8;
                box = level4[genpoint - 28];
            }
            Ingredient ingr = IngredientGenerator.Instance.GenerateIngredient(ingramt);
            ingr.GetComponent<SnapDraggable>().homeBox = box;
            box.allowed = ingr;
            ingr.GetComponent<SnapDraggable>().GoHome();
            genpoint++;
        }
	}

    public void PutInOpen(Ingredient ingredient)
    {
        int ingtotal = ingredient.totalatrb;

        if (ingtotal < 2)
        {
            if (level1.Length < 12)
            {
                DragCatchBox box = level1[level1.Length];
                ingredient.GetComponent<SnapDraggable>().homeBox = box;
                ingredient.GetComponent<SnapDraggable>().GoHome();
            }
        }
    }
}