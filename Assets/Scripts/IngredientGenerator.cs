using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientGenerator : UnitySingleton<IngredientGenerator> {
    List<string> names;
    public Sprite[] sprites;
    Namer namer;
    public GameObject baseIngredient;
	public DragCatchBox home;

    void Start()
    {
        namer = Namer.Instance;
        names = namer.GetAllIngredientNames();
    }

    public Ingredient GenerateIngredient(int maxstats)
    {
        Ingredient newIngredient = ((GameObject)Instantiate(baseIngredient)).GetComponent<Ingredient>();
        int primarystat = Random.Range(0, 4);
        int secondarystat = Random.Range(0, 4);
        while (primarystat == secondarystat)
        {
            secondarystat = Random.Range(0, 4);
        }
        int primevalue = 0;
        int secondvalue = 0;
        for (int i = 0; i < maxstats; i++ )
        {
            int selection = Random.Range(0, 10);
            if (selection < 7)
            {
                primevalue++;
            }
            else
            {
                secondvalue++;
            }
        }
        string name = "";
        if (primevalue > 0)
        {
            name += namer.getStatName(primarystat, primevalue)+" ";
        }
        if (secondvalue > 0)
        {
            name += namer.getStatName(secondarystat, secondvalue)+" ";
        }
        int ingrtype = Random.Range(0, Mathf.Min(names.Count, sprites.Length));
        name += names[ingrtype];
        newIngredient.ingrname = name;
        newIngredient.GetComponent<SpriteRenderer>().sprite = sprites[ingrtype];
        newIngredient.IncreaseStat(primarystat, primevalue);
        newIngredient.IncreaseStat(secondarystat, secondvalue);
        newIngredient.ColorByStats();
        return newIngredient;
    }
}
