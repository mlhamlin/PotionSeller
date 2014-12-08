using UnityEngine;
using System.Collections;

public class Request : MonoBehaviour {

    public int strlevel, intlevel, dexlevel, chalevel;
    public int goldReward;
    public Ingredient[] ingrewards;
    public DragCatchBox[] ingboxes;
    public string reqname;
	public int difficulty;
	
    // Use this for initialization
	void Start () {
        if (ingrewards == null)
            ingrewards = new Ingredient[0];
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isFullfilled(StatBlock stats)
	{
		return stats.meetsRequirement(strlevel, chalevel, intlevel, dexlevel);
	}

    public void Fulfill()
    {
        Shelf shelf = Shelf.Instance;
        PlayerInfo pinfo = PlayerInfo.Instance;
        pinfo.addGold(goldReward);
        foreach (Ingredient reward in ingrewards)
        {
            reward.GetComponent<BoxCollider2D>().enabled = true;
            shelf.PutInOpen(reward);
            reward.transform.position = new Vector2(10, 10);
        }
    }

	public string getText()
	{
		return "str = " + strlevel + "\ncha = " + chalevel + "\nintl = " + intlevel + "\ndex = " + dexlevel;
	}

    public void AddIngredient(Ingredient ing)
    {
        if (ingrewards.Length < ingboxes.Length)
        {
            Ingredient[] newarr = new Ingredient[ingrewards.Length + 1];
            ingrewards.CopyTo(newarr, 0);
            ingrewards = newarr;
            ingrewards[ingrewards.Length - 1] = ing;
            ing.GetComponent<SnapDraggable>().homeBox = ingboxes[ingrewards.Length - 1];
            ing.GetComponent<SnapDraggable>().GoHome();
            ing.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
