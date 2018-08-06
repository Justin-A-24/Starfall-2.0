using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerScript;
	private float landingTime;
	public float timeLimit = 3.0f;
	public bool onMeteor = false;
	public bool timeSet;
	public Rigidbody2D playerBody;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerMovement>();
		playerBody = player.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(onMeteor)
			MeteorTimer();
	}

	void FixedUpdate()
	{
		if(onMeteor)
		{
			MoveTowardCenterOfMeteor();
		}
	}

	private void MoveTowardCenterOfMeteor()
	{
		Vector3 colliderPosition;
		CircleCollider2D currentCollider;
		currentCollider = gameObject.GetComponent<CircleCollider2D>();
		//if(version == 2)
			colliderPosition = new Vector3(currentCollider.offset.x+0.3f, currentCollider.offset.y+0.5f, 0f);
		//else
		//	colliderPosition = new Vector3(currentCollider.offset.x+0.3f, currentCollider.offset.y+0.3f, 0f);
		playerBody.gravityScale = 0;
        //transform.position = Vector3.SmoothDamp(transform.position, currentStar.transform.position, ref velocity, 0.03f);
		if(playerScript.playerLiving)
			playerScript.transform.position = Vector3.SmoothDamp(playerScript.transform.position, transform.position + colliderPosition, ref velocity, 0.03f);
		if(!playerScript.playerLiving)
		{
			playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
			playerScript.transform.position = Vector3.SmoothDamp(playerScript.transform.position, transform.position + colliderPosition, ref velocity, 0.03f);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		onMeteor = false;
		playerScript.onMeteor = false;
		timeSet = false;
		playerBody.gravityScale = 0.5f;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(!timeSet)
			{
				timeSet = true;
				onMeteor = true;
				landingTime = Time.time;
				StartTimer();
			}
			
			//onComet = true;
			if(Input.GetKeyDown(KeyCode.Space))
			{
				playerScript.canLand = false;
				onMeteor = false;
			}
			else
			{
				playerScript.canLand = true;
				onMeteor = true;
			}
			//StartTimer();
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			//player.transform.position = transform.position;
			onMeteor = true;
			StartTimer();
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		onMeteor = false;
	}

	void StartTimer()
	{
		landingTime = Time.time;
	}

	void MeteorTimer()
	{
		if(Time.time > landingTime + timeLimit)
		{
			ReduceFuel();
		}
	}

	void ReduceFuel()
	{
		if(playerScript.jumpsRemaining == 0 && playerScript.playerLiving)
		{
			playerScript.playerLiving = false;
			playerScript.PlayerDies();
		}
		else
		{
			landingTime = Time.time;
			playerScript.jumpsRemaining--;
			playerScript.PlayFire();
		}
	}
}
