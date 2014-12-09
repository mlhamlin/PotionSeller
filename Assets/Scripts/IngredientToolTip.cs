using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngredientToolTip : UnitySingleton<IngredientToolTip> {

	public GameObject tooltip;
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
		if (Input.GetMouseButton (0)) 
		{
			deactivateToolTip();
		}
	}

	public void activateTooltip(Ingredient newIng)
	{
		if (newIng != null)
		{
			tooltip.SetActive(true);

			tooltip.gameObject.transform.SetParent(newIng.gameObject.transform, false);

			if (current != newIng) 
			{
				current = newIng;
				refresh();
			}

			SpriteRenderer spriteRend = current.gameObject.GetComponent<SpriteRenderer> ();
			if (spriteRend != null)
			{
				iconUI.sprite = spriteRend.sprite;
				iconUI.color = spriteRend.color;
				iconUI.enabled = true;
			}

		} else {
			deactivateToolTip();
		}
	}

	public void deactivateToolTip()
	{
		tooltip.SetActive(false);
		tooltip.gameObject.transform.SetParent (gameObject.transform, false);
	}


	public void refresh()
	{
		if (current != null)
		{
			nameUI.text = current.ingrname;
			statsUI.text = "str = " + current.str + "\ncha = " + current.cha + 
				"\nintl = " + current.intl + "\ndex = " + current.dex;
		} else {
			deactivateToolTip();
		}
	}
}
