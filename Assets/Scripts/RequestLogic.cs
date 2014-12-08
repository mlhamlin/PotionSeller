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
    int numbergenerated;

	// Use this for initialization
	void Start () {
		requests = new List<Request>();
		for(int i = 0; i < 3; i++)
		{
			requests.Add(RequestGenerator.Instance.GenerateRequest(i));
		}
        foreach (Ingredient ingrw in requests[1].ingrewards)
        {
            ingrw.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (Ingredient ingrw in requests[2].ingrewards)
        {
            ingrw.GetComponent<SpriteRenderer>().enabled = false;
        }
        numbergenerated = 3;
		cal = Cauldron.Instance;

		currentReq = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (FreeStyle())
		{
			validText.enabled = false;
			currentReqStats.text = "Freestyle!";
			currentReqGoldReward.text = "Gold: " + cal.DummyGoldReward();
		} else {
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

    public void ReplaceCurrentRequest()
    {
        requests[currentReq] = RequestGenerator.Instance.GenerateRequest(1);
        numbergenerated++;
    }

	public bool SelectedCraftable()
	{
		return FreeStyle() || requests[currentReq].isFullfilled(cal.allTheStats);
	}

	public void NextReq()
	{
        if (!FreeStyle())
        {
            foreach (Ingredient ingrw in requests[currentReq].ingrewards)
            {
                ingrw.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
		currentReq += 1;
		if (currentReq >= requests.Count) 
		{
			currentReq = -1;
		}
        if (!FreeStyle())
        {
            foreach (Ingredient ingrw in requests[currentReq].ingrewards)
            {
                ingrw.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
	}

	public void PrevReq()
	{
        if (!FreeStyle())
        {
            foreach (Ingredient ingrw in requests[currentReq].ingrewards)
            {
                ingrw.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
		currentReq -= 1;
		if (currentReq < -1) 
		{
			currentReq = requests.Count - 1;
		}
        if (!FreeStyle())
        {
            foreach (Ingredient ingrw in requests[currentReq].ingrewards)
            {
                ingrw.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
	}

	public void CompletedRequest()
	{
		ReplaceRequest (currentReq);
	}

	public void ReplaceRequest(int i)
	{
		if ((-1 < i) && (i < requests.Count))
		{
			Request holder = requests[i];
			requests[i] = RequestGenerator.Instance.GenerateRequest(holder.difficulty);
			GameObject.Destroy(holder.gameObject);
		}
	}
}
