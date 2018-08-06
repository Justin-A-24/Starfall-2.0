using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointScript : MonoBehaviour 
{
	public SceneLoader loader;
	public GameObject player;
	public Rigidbody2D playerBody;
	public bool sceneLoading = false;
	public PlayerMovement playerData;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();
		playerData = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			playerData.canLand = false;
			playerData.canMove = false;
			playerBody.position = transform.position;
			playerData.StopMovements();
			if(!sceneLoading)
			{
				sceneLoading = true;
				loader.LoadScreen(SceneManager.GetActiveScene().buildIndex +1);
			}
		}
	}		
}
