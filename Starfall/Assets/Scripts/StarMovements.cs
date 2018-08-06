using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovements : MonoBehaviour 
{
	public float movementSpeed;
	GameObject player;
	PlayerMovement playerData;
	public Vector3 topLeft = new Vector3(-1.5f, 7.0f);
	public Vector3 bottomRight = new Vector3(81.0f, -8.0f);
	public Rigidbody2D playerBody;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
		playerBody = player.GetComponent<Rigidbody2D>();
		movementSpeed = Random.Range(0.5f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveStar();
	}

	// move star toward top left of screen
	void MoveStar()
	{
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, topLeft, step);
		if(transform.position == topLeft)
		{	
			SetPlayerOffStar();
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Star"))	
		{
			DestroySlowerStar(other.gameObject);
		}
	}

	void DestroySlowerStar(GameObject other)
	{
		if(other.gameObject.CompareTag("Star"))
		{
			if(movementSpeed < other.GetComponent<StarMovements>().movementSpeed)
			{
				if(playerData.currentStar == gameObject)
					SetPlayerOffStar();
				Destroy(gameObject);
			}
			else
			{
				if(playerData.currentStar == other)
					SetPlayerOffStar();
				Destroy(other);
			}
		}
	}

	void SetPlayerOffStar()
	{
		playerData.onStar = false;
		playerData.canLand = true;
		playerBody.gravityScale = 0.5f;
	}

}
