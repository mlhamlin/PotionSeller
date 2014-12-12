using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ingredient : MonoBehaviour {

    public Color color;
    public int intl, str, cha, dex, cost, level, totalatrb;
    public string ingrname;
    string[] AtrbIncrease = { "intl", "str", "cha", "dex" };

	public void copyIngredient(Ingredient original)
	{
		color = original.color;
		intl = original.intl;
		str = original.str;
		cha = original.cha;
		dex = original.dex;
		cost = original.cost;
		level = original.level;
		totalatrb = original.totalatrb;
	}

    // 0 intl, 1 str, 2 cha, 3 dex
    public void IncreaseStat(int stat, int amt)
    {
        switch (stat)
        {
            case 0:
                IncreaseIntl(amt);
                break;
            case 1:
                IncreaseStr(amt);
                break;
            case 2:
                IncreaseCha(amt);
                break;
            case 3:
                IncreaseDex(amt);
                break;
        }
        totalatrb += amt;
    }

    public void IncreaseIntl(int amt)
    {
        intl += amt;
    }

    public void IncreaseStr(int amt)
    {
        str += amt;
    }

    public void IncreaseDex(int amt)
    {
        dex += amt;
    }

    public void IncreaseCha(int amt)
    {
        cha += amt;
    }

    public void ColorByStats()
    {
        if (totalatrb > 0)
        {
            float strc = (float) str / totalatrb;
			float intc = (float) intl / totalatrb;
			float dexc = (float) dex / totalatrb;
			float chac = (float) cha / totalatrb;
            float red = (strc + chac*0.75f);
            float  green = (dexc + chac*0.75f);
            float blue = intc;
            if (red > 0) red += Random.Range(-0.1f, 0.1f)*red;
            if (green > 0) green += Random.Range(-0.1f, 0.1f)*green;
            if (blue > 0) blue += Random.Range(-0.1f, 0.1f)*blue;
			color = new Color(red * (0.4f + totalatrb / 7f),
                              green * (0.4f + totalatrb / 7f),
                              blue * (0.4f + totalatrb / 7f));
        }
        else
        {
			Debug.Log("totalatrb is 0");
            color = new Color(0, 0, 0);
        }
        GetComponent<SpriteRenderer>().color = color;
    }

	public void OnMouseEnter()
	{
		PlayerInfo pi = PlayerInfo.Instance;
		if (!Input.GetMouseButton (0) && !pi.gameOver)
		{
			if (!pi.storePageShowing)
				IngredientInfoBox.Instance.changeIngredient (this);
			IngredientToolTip.Instance.activateTooltip(this);
		}
	}

	public void OnMouseExit()
	{
		if (!Input.GetMouseButton (0)) 
		{
			if (!PlayerInfo.Instance.storePageShowing)
				IngredientInfoBox.Instance.changeIngredient (null);
			IngredientToolTip.Instance.deactivateToolTip();
		}
	}

	public void OnMouseDown() {
		if (PlayerInfo.Instance.storePageShowing) {
			IngredientInfoBox.Instance.changeIngredient (this);
			Text ct = PlayerInfo.Instance.CostText;
			ct.text = "Cost: " + cost;
			ct.gameObject.SetActive(true);
		}
	}

    public bool equivalentTo(object o2)
    {
        Ingredient ing = o2 as Ingredient;
        return ing != null && totalatrb == ing.totalatrb
            && ing.color == color && ing.ingrname == ingrname
            && GetComponent<SpriteRenderer>().sprite ==
               ing.GetComponent<SpriteRenderer>().sprite;
    }
	public void DetermineCost() {
		//we should relate this somehow to the cauldron value (minus the random)
		cost = (int)((intl + str + cha + dex) * 2 * Random.Range(0.75f, 1.25f));
	}
}
