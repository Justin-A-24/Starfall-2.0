using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScript : MonoBehaviour 
{

	public GameObject player;
	public PlayerMovement playerData;
	public Slider progressBar;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
		progressBar.value = playerData.CalculateProgress();
	}
	

	void FixedUpdate () 
	{
		progressBar.value = playerData.CalculateProgress();
	}
}
