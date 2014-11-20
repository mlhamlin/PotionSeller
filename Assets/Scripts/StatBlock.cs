﻿using UnityEngine;
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
}