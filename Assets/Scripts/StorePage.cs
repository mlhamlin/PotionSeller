using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StorePage : MonoBehaviour {
	List<Ingredient> ingredients = new List<Ingredient>();
	public GameObject topLeftSpot;
	public GameObject bottomRightSpot;
	public const int STORE_PAGE_Z = -3;
	public void reStock() {
		//fill a 4-wide-by-3-high grid based on a pair of rectangle corners
		Vector2 tlp = topLeftSpot.transform.position;
		Vector2 brp = bottomRightSpot.transform.position;
		float distx = (brp.x - tlp.x) / 3.0f;
		float disty = (brp.y - tlp.y) / 2.0f;
		for (int i = 0; i < 3; i += 1) {
			for (int j = 0; j < 4; j += 1) {
				//for now, just do one ingredient per total stat 1-12
				Ingredient ing = IngredientGenerator.Instance.GenerateIngredient(i * 4 + j + 1);
				ingredients.Add(ing);
				ing.transform.position = new Vector3(tlp.x + distx * j, tlp.y + disty * i, STORE_PAGE_Z);
			}
		}
	}
	public void clearStock() {
		foreach(Ingredient i in ingredients) {
			Destroy(i.gameObject);
		}
		ingredients.Clear();
	}
}
