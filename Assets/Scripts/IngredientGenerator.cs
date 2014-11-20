using UnityEngine;
using System.Collections;

public class IngredientGenerator : MonoBehaviour {

    public Sprite[] sprites;

    Ingredient GenerateIngredient()
    {
        return new Ingredient();
    }
}
