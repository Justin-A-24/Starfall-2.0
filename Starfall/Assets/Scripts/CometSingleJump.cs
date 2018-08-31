using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSingleJump : MonoBehaviour
{
    //For get component to the player jump ability
    public bool cometJumpable;
    public GameObject player;
    public CometScript CometScript;
    public PlayerMovement playerScript;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>();
        cometJumpable = true;
        CometScript = gameObject.GetComponent<CometScript>();
        playerScript = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && cometJumpable)
        {
            other.gameObject.GetComponent<PlayerMovement>().haveNotJumpSameStar = true;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space) && cometJumpable && player.GetComponent<PlayerMovement>().jumpsRemaining > 0 && CometScript.frozen == false)
        {
            cometJumpable = false;
            other.gameObject.GetComponent<PlayerMovement>().haveNotJumpSameStar = false;
            other.gameObject.GetComponent<PlayerMovement>().canLand = true;
            other.gameObject.GetComponent<PlayerMovement>().onStar = false;
            other.gameObject.GetComponent<PlayerMovement>().onComet = false;
            other.gameObject.GetComponent<PlayerMovement>().onMeteor = false;
        }
    }
}
