using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour {

    public Color color;
    public int intl, str, cha, dex, cost, level;
    string[] AtrbIncrease = { "intl", "str", "cha", "dex" };

    // 1 intl, 2 str, 3 cha, 4 dex
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
}
