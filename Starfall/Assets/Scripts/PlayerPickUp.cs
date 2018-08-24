using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    //All power up except wormhole
    //Change wormholeDistance for how far the player and wormhole go
    public float wormholeDistance;
    //Change shieldTime float to desire per seconds time
    public float shieldTime;
    public bool shieldBool;
    public bool onWormhole;
    public GameObject player;
    public Rigidbody2D playerBody;
    private float saveShieldTime;
    //Taken from the PlayerMovement Script in MoveWithStar() function
    private Vector3 colliderPosition;
    private CircleCollider2D currentCollider;

    // Use this for initialization
    void Start ()
	{
        player = GameObject.FindWithTag("Player");
	    playerBody = player.GetComponent<Rigidbody2D>();
	    saveShieldTime = shieldTime;
	    shieldBool = false;
	    onWormhole = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (onWormhole)
	    {
	        playerBody.gravityScale = 0;
            //Stay with the collision object "wormhole"
	        transform.position = colliderPosition;
	    }
        //As name impied, deal with the power up of the shield (Mario Star)
        PowerUpShield();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Add other tags to use different "power ups (Effect)", the tag is just an example and is meant to be delete later
        if (other.gameObject.CompareTag("Power Up Shield"))
        {
            //Effects of a powerup
            shieldBool = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Power Up Fuel"))
        {
            //Effects of a powerup
            gameObject.GetComponent<PlayerMovement>().jumpsRemaining = gameObject.GetComponent<PlayerMovement>().maxJumps;
            Destroy(other.gameObject);
        }

        //This if statement give out error message but it is working as expected, let me know if the method is not right and how it can be improve if possible for KingdomCross (Alex Chheng)
        if (other.gameObject.CompareTag("Power Up Wormhole") && other.gameObject.GetComponent<PowerUpWormhole>().wormholeJumpable)
        {
            //Effects of a powerup
            other.transform.position = new Vector3(other.transform.position.x + wormholeDistance, other.transform.position.y, 0);
            colliderPosition = other.transform.position;
            transform.position = new Vector3(other.transform.position.x, other.transform.position.y, 0);
            onWormhole = true;
        }

        //To fix bug when stay on wormhole while collide with other stars
        if (onWormhole && (other.gameObject.CompareTag("Star") || other.gameObject.CompareTag("Comet") || other.gameObject.CompareTag("Meteor")))
        {
            onWormhole = false;
            playerBody.gravityScale = 0.5f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

    }

    void PowerUpShield()
    {
        if (shieldTime <= 0)
        {
            //Located in MeteorScript and CometScript and PlayerMovement for phasethrough star is shieldBool is true, in the function similar as "MoveWithStar"
            shieldBool = false;
        }

        if (shieldBool)
        {
            shieldTime -= Time.deltaTime;
            gameObject.GetComponent<PlayerMovement>().jumpsRemaining = gameObject.GetComponent<PlayerMovement>().maxJumps;
        }
        else if (shieldBool == false)
        {
            shieldTime = saveShieldTime;
        }
    }
}
