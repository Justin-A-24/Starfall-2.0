using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleJumpFromStar : MonoBehaviour
{
    //For get component to the player jump ability
    public bool singleJump;
    public bool jumpable;
    public GameObject player;

	// Use this for initialization
	void Start ()
	{
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>();
	    jumpable = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerStay2D(Collider2D other)
    {
        if (singleJump && other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space) && jumpable)
        {
            jumpable = false;
            other.gameObject.GetComponent<PlayerMovement>().haveNotJumpSameStar = false;
        }
    }
}
