using UnityEngine;
using System.Collections;

public class Request : MonoBehaviour {

    public int strlevel, intlevel, dexlevel, chalevel;
    public int goldReward;
    public Ingredient[] ingrewards;
    public string reqname;
	public int difficulty;
	
    // Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isFullfilled(StatBlock stats)
	{
		return stats.meetsRequirement(strlevel, chalevel, intlevel, dexlevel);
	}

    void Fulfill()
    {
        Shelf shelf = Shelf.Instance;
        PlayerInfo pinfo = PlayerInfo.Instance;
        pinfo.Gold += goldReward;
        foreach (Ingredient reward in ingrewards)
        {
            shelf.PutInOpen(reward);
        }
    }

	public string getText()
	{
		return "str = " + strlevel + "\ncha = " + chalevel + "\nintl = " + intlevel + "\ndex = " + dexlevel;
	}
}
