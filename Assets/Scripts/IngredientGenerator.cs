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
        if (namer == null)
        {
            namer = Namer.Instance;
            names = namer.GetAllIngredientNames();
        }
        Ingredient newIngredient = ((GameObject)Instantiate(baseIngredient)).GetComponent<Ingredient>();
        int primarystat = Random.Range(0, 4);
        int secondarystat = Random.Range(0, 4);
        int[] stats = new int[] {0, 0, 0, 0};
        while (primarystat == secondarystat)
        {
            secondarystat = Random.Range(0, 4);
        }
        int i = 0;
        while (i < maxstats)
        {
            int selection = Random.Range(0, 15);
            if (selection < 5/maxstats)
            {
                stats[Random.Range(0, 3)]--;
                i--;
            }
            else if (selection < 9)
            {
                stats[primarystat]++;
                i++;
            }
            else
            {
                stats[secondarystat]++;
                i++;
            }
        }
        string name = "";
        if (stats[primarystat] > 0)
        {
            name += namer.getStatName(primarystat, stats[primarystat])+" ";
        }
        if (stats[secondarystat] > 0)
        {
            name += namer.getStatName(secondarystat, stats[secondarystat])+" ";
        }
        int ingrtype = Random.Range(0, Mathf.Min(names.Count, sprites.Length));
        name += names[ingrtype];
        newIngredient.ingrname = name;
        newIngredient.GetComponent<SpriteRenderer>().sprite = sprites[ingrtype];
        for (int j = 0; j < 4; j++)
        {
            newIngredient.IncreaseStat(j, stats[j]);
        }
        newIngredient.ColorByStats();
        return newIngredient;
    }
}
