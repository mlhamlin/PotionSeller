using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Cauldron : UnitySingleton<Cauldron> {

	public List<Ingredient> ingredients; //will be ingredient class
	public StatBlock allTheStats;
	const int MAX_INGREDIENTS = 9;
	public DragCatchBox[] boxes;
	public Text statsText;

	// Use this for initialization
	void Start () {
		allTheStats = new StatBlock ();
		ingredients = new List<Ingredient> ();

		foreach (DragCatchBox box in boxes)
		{
			box.dragAdded += attemptAddIngredient;
			box.dragRemoved += attemptRemoveIngredient;
		}

		UpdateStatsTextUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void attemptAddIngredient(SnapDraggable drag)
	{
		Ingredient ing = drag.GetComponent<Ingredient>();
		if (ing != null)
		{
			addIngredient(ing);
		}
	}

	public bool addIngredient(Ingredient ing)
	{
		if (ingredients.Count <= MAX_INGREDIENTS) 
		{
			ingredients.Add(ing);
			allTheStats.addValues(ing);
			allTheStats.debugStats();
			UpdateStatsTextUI();
			return true;
		}

		return false;
	}

	public void attemptRemoveIngredient(SnapDraggable drag)
	{
		Ingredient ing = drag.GetComponent<Ingredient>();
		if (ing != null)
		{
			removeIngredient(ing);
		}
	}
	
	public bool removeIngredient(Ingredient ing)
	{
		if (ingredients.Contains (ing)) 
		{
			ingredients.Remove(ing);
			allTheStats.removeValues(ing);
			allTheStats.debugStats();
			UpdateStatsTextUI();
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

	public void UpdateStatsTextUI()
	{
		if (statsText != null)
		{
			statsText.text = allTheStats.getText();
		}
	}
	
}
