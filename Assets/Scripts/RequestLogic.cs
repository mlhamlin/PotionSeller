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
	public GameObject Panel; 

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
			ingrw.gameObject.SetActive(false);
        }
        foreach (Ingredient ingrw in requests[2].ingrewards)
        {
            ingrw.GetComponent<SpriteRenderer>().enabled = false;
			ingrw.gameObject.SetActive(false);
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
		Request holder = requests [currentReq];
        requests[currentReq] = RequestGenerator.Instance.GenerateRequest(holder.difficulty);
		GameObject.Destroy(holder.gameObject);
        numbergenerated++;
    }

	public bool SelectedCraftable()
	{
		return FreeStyle() || requests[currentReq].isFullfilled(cal.allTheStats);
	}

	//Wraps around
	public void NextReq()
	{
        if (!FreeStyle())
        {
            foreach (Ingredient ingrw in requests[currentReq].ingrewards)
            {
                ingrw.GetComponent<SpriteRenderer>().enabled = false;
				ingrw.gameObject.SetActive(false);
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
				ingrw.gameObject.SetActive(true);
            }
        }
	}

	//Wraps around
	public void PrevReq()
	{
        if (!FreeStyle())
        {
            foreach (Ingredient ingrw in requests[currentReq].ingrewards)
            {
                ingrw.GetComponent<SpriteRenderer>().enabled = false;
				ingrw.gameObject.SetActive(false);
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
				ingrw.gameObject.SetActive(true);
            }
        }
	}

	//Clamps instead of wrapping.
	public void SelectReq(int i)
	{
		if (!FreeStyle())
		{
			foreach (Ingredient ingrw in requests[currentReq].ingrewards)
			{
				ingrw.GetComponent<SpriteRenderer>().enabled = false;
				ingrw.gameObject.SetActive(false);
			}
		}

		currentReq = i;

		currentReq = Mathf.Max (currentReq, -1);
		currentReq = Mathf.Min (currentReq, requests.Count);

		if (!FreeStyle())
		{
			foreach (Ingredient ingrw in requests[currentReq].ingrewards)
			{
				ingrw.GetComponent<SpriteRenderer>().enabled = true;
				ingrw.gameObject.SetActive(true);
			}
		}

		ToggleSelectionWindow ();
	}

	public void ToggleSelectionWindow()
	{
		Panel.SetActive (!Panel.activeSelf);
	}
}
