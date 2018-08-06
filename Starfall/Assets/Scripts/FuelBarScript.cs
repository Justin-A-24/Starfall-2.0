using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarScript : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerData;
	public Slider fuelBar;
	public Slider miniFuelBar;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
		SetFuelBar();
		fuelBar.maxValue = playerData.maxJumps;
		miniFuelBar.maxValue = playerData.maxJumps;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		SetFuelBar();
	}

	void SetFuelBar()
	{
		fuelBar.value = playerData.jumpsRemaining;
		miniFuelBar.value = playerData.jumpsRemaining;
	}

}
