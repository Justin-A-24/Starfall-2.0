using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovements : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerScript;
	public float movementSpeed;
	public float startPosition;
	public Vector3 endPosition;
	private float leftBound = -7.0f;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerScript = player.GetComponent<PlayerMovement>();
		endPosition = new Vector3(leftBound, transform.position.y);
		movementSpeed = Random.Range(0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveAsteroid();
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Star"))	
			DestroyBoth(other.gameObject);
		if(other.gameObject.CompareTag("Player"))	
		{
			if(playerScript.playerLiving && playerScript.playerProgress < 1.0f)
			{
				playerScript.playerLiving = false;
				playerScript.PlayerDies();
			}

		}
		if(other.gameObject.CompareTag("Asteroid"))
			DestroyBoth(other.gameObject);
	}

	void MoveAsteroid()
	{
		float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
		if(transform.position.x == leftBound)
			Destroy(gameObject);
	}

	void DestroyBoth(GameObject other)
	{
		Destroy(other);
		Destroy(gameObject);
	}
}
