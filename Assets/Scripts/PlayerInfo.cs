﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfo : UnitySingleton<PlayerInfo> {

	public int Gold;
	public Text GoldText;
	public Text GoldText2;
	public const int TURNS_PER_DAY = 10;
	public const int EXPENSES_PER_DAY = 1000;
	public int turn;
	public Text TurnNumbText;
	public int day;
	public Text DayNumbText;
	public GameObject YouLoseDisplay;
	public Text SurvivedText;
	public bool gameOver;
	public StorePage StorePage;
	public bool storePageShowing;

	// Use this for initialization
	void Start () {
		updateUI();
		storePageShowing = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addGold(int g)
	{
		Gold += g;
		updateUI();
	}

	public string getGoldText()
	{
		return "Gold: " + Gold;
	}

	public void updateUI()
	{
		if (GoldText != null)
		{
			GoldText.text = getGoldText();
			GoldText2.text = GoldText.text;
		}

		if (TurnNumbText != null)
		{
			TurnNumbText.text = "Turn: " + turn;
		}

		if (DayNumbText != null)
		{
			DayNumbText.text = "Day: " + day;
		}
	}

	public void turnPassed()
	{
		turn++;

		if (turn >= TURNS_PER_DAY)
		{
			turn = 0;
			day++;
			Gold -= EXPENSES_PER_DAY;
			if (Gold < 0)
			{
				YouLose();
			} else {
				StorePage.gameObject.SetActive(true);
				StorePage.reStock();
				storePageShowing = true;
				//TODO: Go to Shop.
				Debug.Log("And now... the Shop!");
			}
		}

		updateUI ();
	}

	public void YouLose()
	{
		YouLoseDisplay.SetActive (true);
		SurvivedText.text = "" + (day - 1);
		gameOver = true;
	}

	public void Reset()
	{
		//TODO: Reset game?
	}

	public void LeaveStorePage()
	{
		StorePage.gameObject.SetActive(false);
		StorePage.clearStock();
		storePageShowing = false;
	}
}
