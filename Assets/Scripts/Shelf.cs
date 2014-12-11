using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shelf : UnitySingleton<Shelf> {

    public ShelfSpot[] level1;
    public ShelfSpot[] level2;
    public ShelfSpot[] level3;
    public ShelfSpot[] level4;
    int[] genpoints;
    int totalboxes;

	// Use this for initialization
    void Start() 
    {
        totalboxes = level1.Length + level2.Length + level3.Length + level4.Length;
        genpoints = new int[4] {0, 0, 0, 0};
    }
	
	// Update is called once per frame
	void Update () {
        if (genpoints[0] < 8)
        {
            ShelfSpot sbox = level1[genpoints[0]];
            int ingramt = 0;
                ingramt = 2;
            Ingredient ingr = IngredientGenerator.Instance.GenerateIngredient(ingramt);
            sbox.InitBox(ingr);
            genpoints[0]++;
        }
	}

    public void PutInOpen(Ingredient ingredient)
    {
        bool hasSpace = true;
        ShelfSpot box = level1[0];
        if (ingredient.totalatrb < 3)
        {
            if (genpoints[0] < level1.Length)
            {
                box = level1[genpoints[0]];
                hasSpace = true;
                genpoints[0]++;
            }
        }
        else if (ingredient.totalatrb < 6)
        {
            if (genpoints[1] < level2.Length)
            {
                box = level2[genpoints[1]];
                hasSpace = true;
                genpoints[1]++;
            }
        }
        else if (ingredient.totalatrb < 9)
        {
            if (genpoints[2] < level3.Length)
            {
                box = level3[genpoints[2]];
                hasSpace = true;
                genpoints[2]++;
            }
        }
        else
        {
            if (genpoints[3] < level4.Length)
            {
                box = level4[genpoints[3]];
                hasSpace = true;
                genpoints[3]++;
            }
        }
        if (hasSpace)
            box.InitBox(ingredient);
    }

    public List<Ingredient> GetIngredients()
    {
        List<Ingredient> li = new List<Ingredient>();
        foreach (ShelfSpot ss in level1)
        {
            if (ss.ingr != null)
                li.Add(ss.ingr);
        }
        foreach (ShelfSpot ss in level2)
        {
            if (ss.ingr != null)
                li.Add(ss.ingr);
        }
        foreach (ShelfSpot ss in level3)
        {
            if (ss.ingr != null)
                li.Add(ss.ingr);
        }
        foreach (ShelfSpot ss in level4)
        {
            if (ss.ingr != null)
                li.Add(ss.ingr);
        }
        return li;
    }
}