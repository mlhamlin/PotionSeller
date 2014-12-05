using UnityEngine;
using System.Collections;

public class RequestGenerator : UnitySingleton<RequestGenerator> {

	public GameObject baseRequest;
    public DragCatchBox[] boxes;

	private const string LOREM = "Lorem ipsum dolor sit amet consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua Ut enim ad minim veniam quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur Excepteur sint occaecat cupidatat non proident sunt in culpa qui officia deserunt mollit anim id est laborum";
	private string[] LoremBits;

	// Use this for initialization
	void Start () {
		GenerateLorem();
	}
	
	// Update is called once per frame
	void Update () {
	
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
		int totalStats = 1;
		switch(difficulty)
		{
			case 0:
				totalStats = Random.Range(2, 5);
				break;
			case 1:
				totalStats = Random.Range(5, 10);
				break;
			case 2:
				totalStats = Random.Range(10, 16);
				break;
			default:
				break;
		}

		int factor = 3 - difficulty;
		while (totalStats >= factor)
		{
			newReq = IncreaseRequirement(newReq, Random.Range(0, 4), factor);
			totalStats -= factor;
		}
		newReq = IncreaseRequirement(newReq, Random.Range(0, 4), totalStats);

		newReq.goldReward = 25 + (100 * difficulty) + Random.Range(0, 100);
        if (Random.Range(1, 1) > 0)
        {
            Ingredient ing = IngredientGenerator.Instance.GenerateIngredient(Mathf.Max(Random.Range(difficulty*3+2, difficulty*4+2), 0));
            newReq.AddIngredient(ing);
            ing.GetComponent<SnapDraggable>().enabled = false;
        }

		int partOne = Random.Range(0, LoremBits.Length);
		int partTwo = Random.Range(0, LoremBits.Length);
		newReq.reqname = LoremBits[partOne] + " " + LoremBits[partTwo];

		return newReq;
	}

	public Request IncreaseRequirement(Request req, int stat, int amt)
	{
		switch (stat)
		{
			case 0:
				req.strlevel += amt;
				break;
			case 1:
				req.chalevel += amt;
				break;
			case 2:
				req.intlevel += amt;
				break;
			case 3:
				req.dexlevel += amt;
				break;
		}

		return req;
	}

}
