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
	public Text valueText;
	public Button craftButton;
	public bool doneFirst;

	// Use this for initialization
	void Start () {
		allTheStats = new StatBlock ();
		ingredients = new List<Ingredient> ();

		foreach (DragCatchBox box in boxes)
		{
			box.dragAdded += attemptAddIngredient;
			box.dragRemoved += attemptRemoveIngredient;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!doneFirst) 
		{
			UpdateUI();
			doneFirst = true;
		}
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
			UpdateUI();
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
			UpdateUI();
			return true;
		}

		return false;
	}

	public void craft()
	{
		//TODO: Improve this\
		if (RequestLogic.Instance.FreeStyle())
		{
			PlayerInfo.Instance.addGold(DummyGoldReward());
		} else {
            //PlayerInfo.Instance.addGold(RequestLogic.Instance.CurrentReq.goldReward);
            RequestLogic.Instance.CurrentReq.Fulfill();
            print("fulfilled!");
            RequestLogic.Instance.ReplaceCurrentRequest();
		}
        print("new request!");
		foreach (Ingredient ing in ingredients)
		{
			DestroyObject(ing.gameObject);
		}
        print("Ingredients gone!");
		ingredients.Clear ();
        print("Ingredients cleared!");
		allTheStats.clear ();
        print("Stats cleared!");
		foreach (DragCatchBox box in boxes)
		{
			box.ForgetItAll();
		}

		RequestLogic.Instance.CompletedRequest ();

        print("Boxes forgotten!");
		UpdateUI ();
        print("Updated UI!");
	}

	public void UpdateUI()
	{
		if (statsText != null)
		{
			statsText.text = allTheStats.getText();
		}

		if (valueText != null)
		{
			valueText.text = "Value: " + DummyGoldReward();
		}

		craftButton.interactable = RequestLogic.Instance.SelectedCraftable();
	}

	//This does stuff, attempts to reward more for better ingredient use and better sum.
	public int DummyGoldReward()
	{
		if (ingredients.Count > 0)
		{
			return Mathf.Max (0, (allTheStats.getStatSum() / ingredients.Count) * allTheStats.getStatSum());
		}

		return 0;
	}

	
}
