﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RequestGenerator : UnitySingleton<RequestGenerator> {

	public GameObject baseRequest;
    public DragCatchBox[] boxes;
    public Shelf shelf;

	private const string LOREM = "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Ut enim ad minim veniam quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur Excepteur sint occaecat cupidatat non proident sunt in culpa qui officia deserunt mollit anim id est laborum";
	private string[] LoremBits;

	// Use this for initialization
	void Start () {
		GenerateLorem();
	}

	private void GenerateLorem()
	{
		LoremBits = LOREM.Split(' ');
	}

	//Note not guarenteed to be craftable or profitable.
	//This is just a placeholder, difficulty is 0, 1 or 2
	public Request GenerateRequest(int difficulty)
	{
		if(LoremBits == null)
		{
			GenerateLorem();
		}

		Request newReq = ((GameObject)Instantiate(baseRequest)).GetComponent<Request>();
        newReq.ingboxes = boxes;
        List<Ingredient> ingredients = shelf.GetIngredients();
        int totalstats = 0;
        int[] stats = new int[] { 0, 0, 0, 0 };
        if (ingredients.Count == 0)
        {
            for (int i = 0; i < difficulty; i++)
            {
                stats[Random.Range(0, 3)] += Random.Range(1, 3);
            }
        }
        else
        {
            for (int i = 0; i < difficulty; i++)
            {
                Ingredient ingr = ingredients[Random.Range(0, ingredients.Count - 1)];
                stats[0] += ingr.intl;
                stats[1] += ingr.str;
                stats[2] += ingr.cha;
                stats[3] += ingr.dex;
            }
        }
        JitterStats(stats);
        int maxatrb = 0;
        int maxamt = 0;
        for (int i = 0; i < 4; i++)
        {
            newReq = IncreaseRequirement(newReq, i, stats[i]);
            totalstats += stats[i];
            if (stats[i] > maxamt)
            {
                maxatrb = i;
                maxamt = stats[i];
            }
        }
        int ningredients = Random.Range(0, difficulty) / 3;
        for (int i = 0; i < ningredients; i++)
        {
            Ingredient ing = IngredientGenerator.Instance.GenerateIngredient(Mathf.Max(totalstats/2, 1));
            newReq.AddIngredient(ing);
            ing.GetComponent<SnapDraggable>().dragEnabled = false;
        }   
        newReq.goldReward = Mathf.Max(totalstats * 50 - newReq.ingrewards.Length * 75 + Random.Range(0, 100), 0);

		newReq.reqname = Namer.Instance.getPotionName(maxatrb, totalstats);

		newReq.difficulty = difficulty;

		return newReq;
	}

	public Request IncreaseRequirement(Request req, int stat, int amt)
	{
		switch (stat)
		{
			case 0:
				req.intlevel += amt;
				break;
			case 1:
				req.strlevel += amt;
				break;
			case 2:
				req.chalevel += amt;
				break;
			case 3:
				req.dexlevel += amt;
				break;
		}

		return req;
	}

    void JitterStats(int[] stats)
    {
        for (int i = 0; i < 4; i++)
        {
            if (stats[i] < 0)
                stats[i] = 0;
            else
                stats[i] += (Random.Range(0, 2) - 1);
        }
    }

}
