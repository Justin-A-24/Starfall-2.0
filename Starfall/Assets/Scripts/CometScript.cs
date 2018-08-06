using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometScript : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerScript;
	public bool onComet = false;
	public float landingTime;
	private float exitTime;
	public float timeLimit = 1.5f;
	public float freezeDuration = 3.0f;
	public float frozenStartTime;
	public float frozenEndTime;
	public bool frozen = false;
	public bool timerOn = false;
	private Vector3 velocity = Vector3.zero;
	public Rigidbody2D playerBody;
	public bool timeSet;
	public int version;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerMovement>();
		playerBody = player.GetComponent<Rigidbody2D>();
		version = transform.GetComponent<CometMovements>().cometVersion;
		frozen = false;
		timeLimit = 1.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckFrozen();
	}

	void FixedUpdate()
	{
		if(onComet)
		{
			MoveTowardCenterOfComet();
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			print("collision");
			onComet = true;
			playerScript.canLand = false;
			StartCometTimer();
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(!timeSet)
			{
				timeSet = true;
				landingTime = Time.time;
				frozenStartTime = landingTime + timeLimit;
				frozenEndTime = frozenStartTime + freezeDuration;
			}
			print("collision");
			//onComet = true;
			if(Input.GetKeyDown(KeyCode.Space))
			{
				playerScript.canLand = false;
				onComet = false;
			}
			else
			{
				playerScript.canLand = true;
				onComet = true;
			}
			StartCometTimer();
		}
	}

	private void MoveTowardCenterOfComet()
	{
		Vector3 colliderPosition;
		CircleCollider2D currentCollider;
		currentCollider = gameObject.GetComponent<CircleCollider2D>();
		/*if(version == 2)
			colliderPosition = new Vector3(currentCollider.offset.x+0.1f, currentCollider.offset.y+0.1f, 0f);
		else
			colliderPosition = new Vector3(currentCollider.offset.x+0.3f, currentCollider.offset.y+0.3f, 0f);
		playerBody.gravityScale = 0;*/
        //transform.position = Vector3.SmoothDamp(transform.position, currentStar.transform.position, ref velocity, 0.03f);
		//playerScript.transform.position = Vector3.SmoothDamp(playerScript.transform.position, transform.position + colliderPosition, ref velocity, 0.03f);
		if(version == 2)
			colliderPosition = new Vector3(currentCollider.offset.x+0.1f + transform.position.x, currentCollider.offset.y+0.1f + transform.position.y, 0f);
		else
			colliderPosition = new Vector3(currentCollider.offset.x+0.3f+ transform.position.x, currentCollider.offset.y+0.3f+ transform.position.y, 0f);
		playerScript.transform.position = colliderPosition;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		onComet = false;
		playerScript.onComet = false;
		timeSet = false;
		playerBody.gravityScale = 0.5f;
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		onComet = false;
		//EndTimer();
	}

	/*void StartCometTimer()
	{
		if(onComet)
		{
			landingTime = Time.time;
			frozenStartTime = landingTime + timeLimit;
			frozenEndTime = frozenStartTime + freezeDuration;
		}
	}*/

	void StartCometTimer()
	{
		if(onComet)
		{
			if(!timeSet)
			{
				landingTime = Time.time;
				frozenStartTime = landingTime + timeLimit;
				frozenEndTime = frozenStartTime + freezeDuration;
			}
		}
	}

	/*void EndTimer()
	{
		exitTime = Time.time;
	}*/

	/*void startFrozenTimer()
	{
		if(Time.time < frozenEndTime)
			frozen = true;
		if(Time.time > frozenEndTime)
		{
			print("Player Unfrozen");
			frozen = false;
		}
	}*/

	/*void ResetTimers()
	{
		StartCometTimer();
	}*/

	void CheckFrozen()
	{
		if(onComet)
		{
			if(Time.time > frozenStartTime && Time.time < frozenEndTime )
			{
				//print(Time.time);
				frozen = true;
			}
			else			
			{
				frozen = false;
				//StartCometTimer();
				//frozenStartTime = Time.time + 1.0f;
			}
		}
		if(frozen)
		{
			frozen = true;
			//print("Frozen");
			playerScript.FreezePlayer();
			//playerScript.PlayerDies();
		}
		if(!frozen && onComet)
		{
			frozen = false;
			//print("Unfrozen");
			playerScript.UnfreezePlayer();
			//StartCometTimer();
		}
	}
}
