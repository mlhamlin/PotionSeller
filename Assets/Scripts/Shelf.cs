using UnityEngine;
using System.Collections;

public class Shelf : UnitySingleton<Shelf> {

    public ShelfSpot[] level1;
    public ShelfSpot[] level2;
    public ShelfSpot[] level3;
    public ShelfSpot[] level4;
    int genpoint = 0;
    int totalboxes;

	// Use this for initialization
    void Start() 
    {
        totalboxes = level1.Length + level2.Length + level3.Length + level4.Length;
    }
	
	// Update is called once per frame
	void Update () {
        if (genpoint < 8)
        {
            ShelfSpot sbox = level1[genpoint];
            int ingramt = 0;
                ingramt = 2;
            Ingredient ingr = IngredientGenerator.Instance.GenerateIngredient(ingramt);
            sbox.InitBox(ingr);
            genpoint++;
        }
	}

    public void PutInOpen(Ingredient ingredient)
    {
        int ingtotal = ingredient.totalatrb;

        if (genpoint < totalboxes)
        {
            ShelfSpot box = level1[0];
            if (genpoint < 12)
            {
                box = level1[genpoint];
            }
            else if (genpoint < 20)
            {
                box = level2[genpoint-12];
            }
            else if (genpoint < 28)
            {
                box = level3[genpoint-20];
            }
            else if (genpoint < 32)
            {
                box = level4[genpoint-28];
            }
            box.InitBox(ingredient);

            genpoint++;
        }
    }
}