using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public float shieldTime;
    public bool shieldBool;
    private float saveShieldTime;

	// Use this for initialization
	void Start ()
	{
	    saveShieldTime = shieldTime;
	    shieldBool = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(shieldTime);
        if (shieldTime <= 0)
	    {
	        shieldBool = false;
	        //gameObject.GetComponent<PlayerMovement>() = true;
	    }

	    if (shieldBool)
	    {
	        shieldTime -= Time.deltaTime;
            gameObject.GetComponent<PlayerMovement>().jumpsRemaining = gameObject.GetComponent<PlayerMovement>().maxJumps;
        } else if (shieldBool == false)
	    {
	        shieldTime = saveShieldTime;
	    }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Add other tags to use different "power ups (Effect)", the tag is just an example and is meant to be delete later
        if (other.gameObject.CompareTag("Power Up Shield"))
        {
            //Effects of a powerup
            Debug.Log("Shield Power Up");
            shieldBool = true;
            Debug.Log(shieldBool);
        }

        if (other.gameObject.CompareTag("Power Up Fuel"))
        {
            //Effects of a powerup
            gameObject.GetComponent<PlayerMovement>().jumpsRemaining = gameObject.GetComponent<PlayerMovement>().maxJumps;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Power Up Wormhole1"))
        {
            //Effects of a powerup
            Debug.Log("Wormhole1 Power Up");
        }
    }
}
