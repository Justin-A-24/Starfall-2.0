using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpCounting : MonoBehaviour {
	public int count;
	public Text countText; 
	public GameObject player;
	public PlayerMovement playerData;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
	}

	void FixedUpdate () 
	{
		count = playerData.jumpsRemaining;
		SetCountText ();
	}

	void SetCountText()
	{
		countText.text = "Jumps Remaining: " + count.ToString();
	}
}
