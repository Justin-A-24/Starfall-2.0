﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour 
{

	public GameObject player;
	public GameObject currentStar;
	public Rigidbody2D playerBody;
	public SpriteRenderer playerRenderer;
	public Sprite deathSprite;
	public Sprite frozenSprite;
	public Sprite idleSprite;
	public bool onStar = false;
	public bool canLand = true;
	public float speed = 10.0f;
	public float jumpForce;
	private float moveInput;
	private bool facingRight = false;
	public int maxJumps;
	public int jumpsRemaining;
	public float playerProgress;
	public float levelStartPos = -35.0f;
	public float levelEndPos = 45.0f;
	public Animator myAnim;
	public bool falling;
	public bool movingToCenter;
	private Vector3 velocity = Vector3.zero;
	public bool canMove = true;
	public float gameDuration = 120.0f;
	public float timeLeft;
	public GameObject pausePanel;
	public GameObject diedMenu;
	public AudioSource deathAudio;
	public SoundEffectsScript gameplaySound;
	public AudioClip gameplayAudioClip;
	public bool playerLiving = true;
	public ParticleSystem fireParticles;
	public ParticleSystem iceParticles;
	public Sprite burntSprite;
	public bool isFrozen;
	public ParticleSystem jetPackParticles;
	public bool onComet;
	public GameObject currentComet;
	public GameObject comet;
	public bool onMeteor;
	public GameObject mainCamera;
	public AudioSource bgPlayer;
    public bool haveNotJumpSameStar = true;
    public bool AlternateMovement;
    public float speedOnStar = 0.1f;
    private float speedSave;
	//public AudioClip explosionSound;


	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.FindWithTag("MainCamera");
		bgPlayer = mainCamera.GetComponent<BGMusicScript>().audioPlayer;
		player = GameObject.FindWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();
		playerRenderer = player.GetComponent<SpriteRenderer>();
        //Keeping storage of gameObject "star"
        //Need to get script from star "SingleJumpFromStar"
        myAnim = player.GetComponent<Animator>();
		levelStartPos = GameObject.FindWithTag("Startpoint").transform.position.x;
		levelEndPos = GameObject.FindWithTag("Endpoint").transform.position.x;
		jumpsRemaining = maxJumps;
		canMove = true;
		timeLeft = gameDuration;
		pausePanel = GameObject.FindWithTag("Pause Panel");
		pausePanel.SetActive(false);
		diedMenu = GameObject.FindWithTag("Died Menu");
		diedMenu.SetActive(false);
		fireParticles = GameObject.FindWithTag("FireParticles").transform.GetComponent<ParticleSystem>();
		fireParticles.Stop();
		iceParticles = GameObject.FindWithTag("IceParticles").transform.GetComponent<ParticleSystem>();
		iceParticles.Stop();
		isFrozen = false;
		jetPackParticles = GameObject.FindWithTag("JetPackparticles").transform.GetComponent<ParticleSystem>();
		jetPackParticles.Stop();
		jetPackParticles.Clear();
		gameplaySound = GameObject.FindWithTag("SFX Player").GetComponent<SoundEffectsScript>();
        //To save the custom speed in inspector
	    speedSave = speed;
    }
	
	void FixedUpdate()
	{
		UpdateTime();
		moveInput = Input.GetAxis("Horizontal");
		if(canMove)
		{
			playerBody.velocity = new Vector2(moveInput * speed, playerBody.velocity.y);
			if(!facingRight && moveInput > 0)
				Flip();
			else if(facingRight && moveInput < 0)
				Flip();
		}

		myAnim.SetBool ("falling", falling);
		myAnim.SetBool ("onStar", onStar);
		myAnim.SetBool ("playerLiving", playerLiving);
		HandleMovements();
		//HandleInput();
	}
	// Update is called once per frame
	void Update () 
	{
		HandleInput();
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}

    //When player enter trigger object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SingleJumpFromStar>().starJumpable || other.gameObject.GetComponent<CometSingleJump>().cometJumpable || other.gameObject.GetComponent<MeteorSingleJump>().meteorJumpable)
        {
            haveNotJumpSameStar = true;
        }
        //To stop all movement upon entry
        if (AlternateMovement)
        {
            playerBody.velocity = Vector3.zero;
        }
    }

	private void OnTriggerStay2D(Collider2D other)
	{
		// set current star player is riding on
		if(other.gameObject.CompareTag("Star") && haveNotJumpSameStar)
			LandOnStar(other.gameObject);
		/*if(other.gameObject.CompareTag("Comet"))
			LandOnComet(other.gameObject);*/
		if(other.gameObject.CompareTag("Wall"))
		{
			if(playerLiving)
			{
				playerLiving = false;
				canMove = false;
				Invoke("PlayerDies", 1.5f);
				Invoke("EnablePauseMenu", 2.0f);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Wall")
			PlayerDies();
	}

    //Alternate movement bool
    private void OnTriggerExit2D(Collider2D other)
    {
        if (AlternateMovement && other.gameObject.tag == "Star")
        {
            playerBody.gravityScale = 0.5f;
            gameplaySound.PlayJetpack();
            jetPackParticles.Clear();
            jetPackParticles.Play();
            //Copy from Handle Input function
            canLand = true;
            onStar = false;
            onComet = false;
            onMeteor = false;
            falling = false;
            //Restore player speed
            speed = speedSave;
        }
    }

    private void LandOnStar(GameObject star)
	{
		if(canLand && !onStar)
		{
			currentStar = star;
			onStar = true;
			RefillFuel();
            HandleMovements();
		}
	}
	// move player position with star position
	private void MoveWithStar()
	{
		//MoveTowardCenter();

		//if(movingToCenter == false)
		playerBody.gravityScale = 0;
		Vector3 colliderPosition;
        CircleCollider2D currentCollider;
	    currentCollider = currentStar.GetComponent<CircleCollider2D>();
        //Only if desire "platform star" then alternate movement be true
        if (AlternateMovement == false)
	    {
	        colliderPosition = new Vector3(currentCollider.offset.x + currentStar.transform.position.x, currentCollider.offset.y + currentStar.transform.position.y, 0f);
	        transform.position = colliderPosition;
        }
        else if (AlternateMovement)
        {
            //colliderPosition = new Vector3(transform.position.x, currentCollider.offset.y + currentStar.transform.position.y, 0f);
            colliderPosition = new Vector3(transform.position.x, transform.position.y, 0f);
            transform.position = colliderPosition;
            speed = speedOnStar;
        }
        else
        {
            Debug.Log("You should not be seeing this, Alternate Movement bool need attention; in MoveWithStar function");
        }
	}

	private void MoveTowardCenter()
	{
		Vector3 colliderPosition;
		CircleCollider2D currentCollider;
		currentCollider = currentStar.GetComponent<CircleCollider2D>();
		colliderPosition = new Vector3(currentCollider.offset.x, currentCollider.offset.y, 0f);
		playerBody.gravityScale = 0;
        //transform.position = Vector3.SmoothDamp(transform.position, currentStar.transform.position, ref velocity, 0.03f);
		transform.position = Vector3.SmoothDamp(transform.position, currentStar.transform.position + colliderPosition, ref velocity, 0.03f);
	}

	// manipulate player position
	private void HandleMovements()
	{
		// if riding a star, move player position with it
		//if(onStar)
		//	MoveTowardCenter();
		if(onStar && haveNotJumpSameStar)
		{
			MoveWithStar();
			playerBody.gravityScale = 0;
			//MoveTowardCenter();
		}
        /*if(onComet)
		{
			playerBody.gravityScale = 0;
			MoveTowardCenterOfComet();
		}*/
    }

	private void HandleInput()
	{
		if(canMove)
		{
			if((Input.GetKeyDown(KeyCode.Space)) && (jumpsRemaining > 0))
			{
				gameplaySound.PlayJetpack();
				jetPackParticles.Clear(); 
				jetPackParticles.Play();
				playerBody.gravityScale = 0.5f;
				canLand = false;
				onStar = false;
				onComet = false;
				onMeteor = false;
				playerBody.velocity = Vector2.up * jumpForce;
				jumpsRemaining--;
				falling = false;
                //Restore movement to the player when jump from star after velocity.zero in void OnTriggerEnter2D
			    speed = speedSave;
            }
			else if(Input.GetKeyUp(KeyCode.Space))
			{
				jetPackParticles.Stop();
				canLand = true;
				falling = true;
			}
		}
		if(!canMove)
			canLand = false;
	}

	private void RefillFuel()
	{
		jumpsRemaining = maxJumps;
	}

	public void PlayerDies()
	{
		//print("Player Died");
		playerLiving = false;
		myAnim.enabled = false;
		playerRenderer.sprite = deathSprite;
		//gameplaySound.PlayOneShot(explosionSound, 0.08f);
		gameplaySound.PlayExplosion();
		PauseMusic();
		PlayDeathMusic();
		playerRenderer.sprite = deathSprite;
		playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
		canMove = false;
		canLand = false;
		diedMenu.SetActive(true);
	}

	public void FreezePlayer()
	{
		//print("Player Froze");
		
		if(!isFrozen)
		{
			isFrozen = true;
			iceParticles.Play();
			playerRenderer.sprite = frozenSprite;
			Invoke("PauseIce", 1.0f);
			Invoke("PlayIce", 3.0f);
			//Invoke("StopIce", 3.0f);
			//Invoke("RevertSprite", 3.0f);
			StopMovements();
		}
	}

	public void StopMovements()
	{
		playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
		canMove = false;
	}

	public void UnfreezePlayer()
	{
		//print("Player Unfroze");
		isFrozen = false;
		if(playerLiving)
		{
			StopIce();
			playerRenderer.sprite = idleSprite;
			playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
			canMove = true;
		}
	}

	public float CalculateProgress()
	{
		playerProgress = transform.position.x / (levelEndPos - levelStartPos);
		return playerProgress;
	}

	public void UpdateTime()
	{
		if(!pausePanel.activeSelf)
		{
			timeLeft -= Time.deltaTime;
			CheckTime();
		}
	}

	public void CheckTime()
	{
		if(timeLeft <= 0.0f)
		{
			PlayerDies();
		}
	}

	public void EnablePauseMenu()
	{
		diedMenu.SetActive(true);
	}

	public void PlayFire()
	{
		if(playerLiving)
		{
			gameplaySound.PlayFuelDepletion();
			fireParticles.Play();
			playerRenderer.sprite = burntSprite;
			Invoke("StopFire", 1.0f);
			Invoke("RevertSprite", 1.0f);
		}
	}

	public void StopFire()
	{
		fireParticles.Stop();
	}

	public void RevertSprite()
	{
		playerRenderer.sprite = idleSprite;
	}

	public void StopIce()
	{
		iceParticles.Stop();
	}

	public void PauseIce()
	{
		iceParticles.Pause();
	}

	public void PlayIce()
	{
		iceParticles.Play();
	}

	public void HandleCometMovements()
	{
		if(onComet)
		{
			//MoveWithStar();
			playerBody.gravityScale = 0;
			MoveTowardCenterOfComet();
		}
	}

	private void LandOnComet(GameObject comet)
	{
		if(canLand && !onComet)
		{
			currentComet = comet;
			onComet = true;
			HandleCometMovements();
		}
	}

	private void MoveTowardCenterOfComet()
	{
		Vector3 colliderPosition;
		CircleCollider2D currentCollider;
		currentCollider = currentComet.GetComponent<CircleCollider2D>();
		colliderPosition = new Vector3(currentCollider.offset.x, currentCollider.offset.y, 0f);
		playerBody.gravityScale = 0;
        //transform.position = Vector3.SmoothDamp(transform.position, currentStar.transform.position, ref velocity, 0.03f);
		transform.position = Vector3.SmoothDamp(transform.position, currentComet.transform.position + colliderPosition, ref velocity, 0.03f);
	}

	public void PauseMusic()
	{
		print("Mute");
		bgPlayer.mute = true;
		bgPlayer.Stop();
	}

	public void PlayDeathMusic()
	{
		deathAudio.Play();
	}
}
