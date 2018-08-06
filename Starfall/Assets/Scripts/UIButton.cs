using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {
	public Button myButton;
	public Sprite notSelectedButton;
	public Sprite selectedButton;
	private int counter = 0;

	void Start () {
		myButton = GetComponent<Button>();
	}
	
	public void changeButton()
	{
		counter++;
		if (counter % 2 == 0)
		{
			myButton.image.overrideSprite = notSelectedButton;
		}
		else
		{
			myButton.image.overrideSprite = selectedButton;
		}
	}
}
