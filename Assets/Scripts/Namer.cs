using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class Namer : UnitySingleton<Namer> {
    const int STATE_INGREDIENT = 0;
    const int STATE_POTION = 0;

    List<List<string>> basenames;
    List<List<string>> strnames;
    List<List<string>> intnames;
    List<List<string>> dexnames;
    List<List<string>> chanames;
    List<string> leveltitles;
    public TextAsset data;
    

    // Use this for initialization
    void Awake()
    {
        basenames = new List<List<string>>();
        strnames = new List<List<string>>();
        intnames = new List<List<string>>();
        chanames = new List<List<string>>();
        dexnames = new List<List<string>>();
        leveltitles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            basenames.Add(new List<string>());
            strnames.Add(new List<string>());
            intnames.Add(new List<string>());
            chanames.Add(new List<string>());
            dexnames.Add(new List<string>());
            leveltitles.Add("");
        }
        string[] lines = data.text.Split('\n');
        int state = 0;
        int level = 0;
        List<List<string>> curatrb = basenames;
        foreach(string s in lines)
        {
            if (s.Contains(":"))
            {
                if (s.Contains("INGREDIENT"))
                    state = STATE_INGREDIENT;
                if (s.Contains("LEVEL"))
                {
					string temp = s.TrimEnd('\r', '\n', ' ');
                    print(temp+" "+temp.Substring(temp.Length - 1));
					level = Convert.ToInt32(temp.Substring(temp.Length - 1));
                }
                if (s.Contains("ATTRIBUTE"))
                {
					string temp = s.TrimEnd('\r', '\n', ' ');
					string attribute = temp.Substring(temp.IndexOf(':')+1, temp.Length - temp.IndexOf(':') - 1);
                    switch (attribute)
                    {
                        case "STRENGTH":
                            curatrb = strnames;
                            break;
                        case "INTELLIGENCE":
                            curatrb = intnames;
                            break;
                        case "DEXTERITY":
                            curatrb = dexnames;
                            break;
                        case "CHARISMA":
                            curatrb = chanames;
                            break;
                        case "BASE":
                            curatrb = basenames;
                            break;
                        default:
                            break;
                    }
                }
                if (s.Contains("TITLE"))
                {
                    leveltitles.Insert(level, s.Substring(s.IndexOf(':') + 1));
                    leveltitles.RemoveAt(level + 1);
                }
            }
            else if (state == STATE_INGREDIENT)
            {
                curatrb[level].Add(s);
            }
        }
    }

    public List<string> GetAllIngredientNames()
    {
        return basenames[0];
    }

    public string getStatName(int stat, int statamount)
    {
        switch(stat)
        {
            case 0:
                return intnames[1][UnityEngine.Random.Range(0, intnames[1].Count)];
            case 1:
                return strnames[1][UnityEngine.Random.Range(0, strnames[1].Count)];
            case 2:
                return chanames[1][UnityEngine.Random.Range(0, chanames[1].Count)];
            case 3:
                return dexnames[1][UnityEngine.Random.Range(0, dexnames[1].Count)];
            default:
                return "A dumb";
        }
    }
}