using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleJumpFromStar : MonoBehaviour
{
    //For get component to the player jump ability
    public bool starSingleJump = true;
    public bool starJumpable;
    public GameObject player;

	// Use this for initialization
	void Start ()
	{
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>();
	    starJumpable = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerStay2D(Collider2D other)
    {
        if (starSingleJump && other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space) && starJumpable)
        {
            starJumpable = false;
            other.gameObject.GetComponent<PlayerMovement>().haveNotJumpSameStar = false;
            other.gameObject.GetComponent<PlayerMovement>().canLand = true;
            other.gameObject.GetComponent<PlayerMovement>().onStar = false;
            other.gameObject.GetComponent<PlayerMovement>().onComet = false;
            other.gameObject.GetComponent<PlayerMovement>().onMeteor = false;
        }
    }
}
