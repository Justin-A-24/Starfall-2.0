using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBarScript : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerData;
	public Slider timerBar;
	public int count;
	public Text countText;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
		timerBar.value = playerData.timeLeft;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timerBar.value = playerData.timeLeft;
	}

		void FixedUpdate () 
	{
		count = (Mathf.RoundToInt(playerData.timeLeft));
		SetCountText ();
	}

	void SetCountText()
	{
		countText.text = "Timer: " + count.ToString();
	}
}
