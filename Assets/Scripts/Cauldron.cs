using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cauldron : UnitySingleton<Cauldron> {

	List<Ingredient> ingredients; //will be ingredient class
	StatBlock allTheStats;
	const int MAX_INGREDIENTS = 9;
	DragCatchBox[] boxes;


	// Use this for initialization
	void Start () {
		allTheStats = new StatBlock ();
		ingredients = new List<Ingredient> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool addIngredient(Ingredient ing)
	{
		if (ingredients.Count <= MAX_INGREDIENTS) 
		{
			ingredients.Add(ing);
			allTheStats.addValues(ing);
			return true;
		}

		return false;
	}

	public bool removeIngredient(Ingredient ing)
	{
		if (ingredients.Contains (ing)) 
		{
			ingredients.Remove(ing);
			allTheStats.removeValues(ing);
			// TODO: Add back to storage???
			return true;
		}

		return false;
	}

	public Potion craft()
	{
		ingredients.Clear ();
		Debug.LogError ("craft not yet implemented");
		return null;
	}
}
