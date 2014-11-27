using UnityEngine;
using System.Collections;

public class StatBlock
{
	int str;
	int cha;
	int intl;
	int dex;

	public void addValues (int addStr, int addCha, int addIntl, int addDex)
	{
		str += addStr;
		cha += addCha;
		intl += addIntl;
		dex += addDex;
	}

	public void addValues (Ingredient ing)
	{
		addValues (ing.str, ing.cha, ing.intl, ing.dex);
	}

	public void removeValues (Ingredient ing)
	{
		addValues (-ing.str, -ing.cha, -ing.intl, -ing.dex);
	}

	public void clear()
	{
		str = 0;
		cha = 0;
		intl = 0;
		dex = 0;
	}

	public bool meetsRequirement (int checkStr, int checkCha, int checkIntl, int checkDex)
	{
		return ((checkStr <= str) && (checkCha <= cha) && (checkIntl <= intl) && (checkDex <= dex));
	}

	public bool meetsReqStr (int stat)
	{
		return stat <= str;
	}

	public bool meetsReqCha (int stat)
	{
		return stat <= cha;
	}

	public bool meetsReqIntl (int stat)
	{
		return stat <= intl;
	}

	public bool meetsReqDex (int stat)
	{
		return stat <= dex;
	}

	public int getStr ()
	{
		return str;
	}

	public int getCha ()
	{
		return cha;
	}

	public int getIntl ()
	{
		return intl;
	}

	public int getDex ()
	{
		return dex;
	}

	public void debugStats()
	{
		Debug.Log("Statblock stats: str = " + str + " cha = " + cha + " intl = " + intl + " dex = " + dex);
	}

	public string getText()
	{
		return "str = " + str + " cha = " + cha + "\nintl = " + intl + " dex = " + dex;
	}

	public int getStatSum()
	{
		return str + cha + intl + dex;
	}
}
