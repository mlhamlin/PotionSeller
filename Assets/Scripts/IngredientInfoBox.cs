using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngredientInfoBox : UnitySingleton<IngredientInfoBox> {

	public Ingredient current;
	public Text nameUI;
	public Text statsUI;
	public Image iconUI;

	// Use this for initialization
	void Start () 
	{
		refresh ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//refresh ();
	}

	public void changeIngredient(Ingredient newIng)
	{
		if (current != newIng) 
		{
			current = newIng;
			refresh();
		}
	}

	public void refresh()
	{
		if (current != null)
		{
			nameUI.text = current.ingrname;
			statsUI.text = "str = " + current.str + "\ncha = " + current.cha + 
				"\nintl = " + current.intl + "\ndex = " + current.dex;
			//iconUI.enabled = true;
			SpriteRenderer spriteRend = current.gameObject.GetComponent<SpriteRenderer> ();
			if (spriteRend != null)
			{
				iconUI.sprite = spriteRend.sprite;
				iconUI.color = spriteRend.color;
				iconUI.enabled = true;
			}
		} else {
			nameUI.text = "";
			statsUI.text = "";
			iconUI.enabled = false;
		}

	}
}
