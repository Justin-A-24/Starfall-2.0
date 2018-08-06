using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggeredUI : MonoBehaviour 
{
	public GameObject uiObject;

	void Start()
	{
		uiObject.SetActive(false);
	}

	void OnTriggerEnter2D (Collider2D player)
	{
		print("Collision");
		if (player.gameObject.tag == "Player")
		{
			uiObject.SetActive(true);
		}
	}

	void OnTriggerExit2D (Collider2D player)
	{
		if (player.gameObject.tag == "Player")
		{
			uiObject.SetActive(false);
		}
	}
}
