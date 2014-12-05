using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShelfUI : MonoBehaviour {

    public string[] names;
    public Text[] UIBoxes;

	// Use this for initialization
	void Start () {
        names = new string[5];
        Namer namer = Namer.Instance;
        for (int i = 0; i < 5; i++)
        {
            names[i] = namer.getLevelName(i);
        }
        for (int i = 0; i < 4; i++)
        {
            UIBoxes[i].text = names[i+1 ];
        }
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
