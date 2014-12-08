using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RequestDisplayBox : MonoBehaviour {
	
	public Text name;
	public Image pic;
	public Text stats;
	public Text goldReward;
	public List<Image> ingRewardPics;
	public int requestNumber;

	RequestLogic reqLog;

	// Use this for initialization
	void Start () {
		reqLog = RequestLogic.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateUI ();
	}

	public void UpdateUI()
	{
		Request req = reqLog.requests [requestNumber];
		name.text = req.reqname;
		stats.text = req.getText();
		goldReward.text = req.goldReward.ToString();
		for (int i = 0; i < req.ingrewards.Length; i++)
		{
			if (i < ingRewardPics.Count)
			{
				SpriteRenderer sprtRend = req.ingrewards[i].gameObject.GetComponent<SpriteRenderer>();
				if (sprtRend != null)
				{
					ingRewardPics[i].sprite = sprtRend.sprite;
					ingRewardPics[i].color = sprtRend.color;
				}
			}
		}
	}

	public void SelectMe()
	{
		reqLog.SelectReq (requestNumber);
	}
}
