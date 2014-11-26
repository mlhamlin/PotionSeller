using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour {

    public Color color;
    public int intl, str, cha, dex, cost, level;
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
        float totalstats = str + intl + dex + cha;
        if (totalstats > 0)
        {
            float strc = str / totalstats;
            float intc = intl / totalstats;
            float dexc = dex / totalstats;
            float chac = cha / totalstats;
            float red = (strc + chac*0.75f);
            float  green = (dexc + chac*0.75f);
            float blue = intc;
            if (red > 0) red += Random.Range(-0.1f, 0.1f)*red;
            if (green > 0) green += Random.Range(-0.1f, 0.1f)*green;
            if (blue > 0) blue += Random.Range(-0.1f, 0.1f)*blue;
            color = new Color(red * (0.4f + totalstats / 7),
                              green * (0.4f + totalstats / 7),
                              blue * (0.4f + totalstats / 7));
        }
        else
        {
            color = new Color(0, 0, 0);
        }
        GetComponent<SpriteRenderer>().color = color;
    }
}
