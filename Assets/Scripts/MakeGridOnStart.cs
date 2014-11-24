using UnityEngine;
using System.Collections;

public class MakeGridOnStart : MonoBehaviour
{

    Ingredient[,] ingredients;
    IngredientGenerator generator;
    public int indexx, indexy = 0;

    // Use this for initialization
    void Start()
    {
        ingredients = new Ingredient[16, 12];
        generator = IngredientGenerator.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (indexx < 16 && indexy == 12)
        {
            indexx++;
            indexy = 0;
        }
        if (indexx < 16 && indexy < 12)
        {
            ingredients[indexx, indexy] = generator.GenerateIngredient((indexx + indexy)/2);
            ingredients[indexx, indexy].transform.position = new Vector2(indexx - 7.5f, indexy - 5.5f);
            indexy++;
        }
    }
}
