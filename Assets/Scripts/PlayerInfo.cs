using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfo : UnitySingleton<PlayerInfo> {

	public int Gold;
	public Text GoldText;

	// Use this for initialization
	void Start () {
		updateUI();
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
		}
	}
}
