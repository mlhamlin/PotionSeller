using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RequestLogic : UnitySingleton<RequestLogic> {

	public List<Request> requests;
	public int currentReq;
	public GameObject requestInfoPanel;
	public Text currentReqStats;
	public Text currentReqGoldReward;
	public Text validText;
	private Cauldron cal;


	// Use this for initialization
	void Start () {
		requests = new List<Request>();
		for(int i = 0; i < 3; i++)
		{
			requests.Add(RequestGenerator.Instance.GenerateRequest(i));
		}

		cal = Cauldron.Instance;

		currentReq = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (FreeStyle())
		{
			requestInfoPanel.SetActive(false);
		} else {
			requestInfoPanel.SetActive(true);
			validText.enabled = requests[currentReq].isFullfilled(cal.allTheStats);
			currentReqStats.text = requests[currentReq].getText();
			currentReqGoldReward.text = "Gold: " + requests[currentReq].goldReward;
		}
	}

	public bool FreeStyle()
	{
		return currentReq == -1;
	}

	public int CurrentGoldReward()
	{
		return requests[currentReq].goldReward;
	}

	public Request CurrentReq
	{
		get 
		{
			return requests[currentReq];
		}
	}

	public bool SelectedCraftable()
	{
		return FreeStyle() || requests[currentReq].isFullfilled(cal.allTheStats);
	}
}
