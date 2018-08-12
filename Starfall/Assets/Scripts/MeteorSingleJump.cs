using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSingleJump : MonoBehaviour
{
    //For get component to the player jump ability
    public bool meteorSingleJump = true;
    public bool meteorJumpable;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovement>();
        meteorJumpable = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (meteorSingleJump && other.gameObject.tag == "Player" && meteorJumpable)
        {
            other.gameObject.GetComponent<PlayerMovement>().haveNotJumpSameStar = true;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (meteorSingleJump && other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Space) && meteorJumpable && player.GetComponent<PlayerMovement>().jumpsRemaining > 0)
        {
            meteorJumpable = false;
            other.gameObject.GetComponent<PlayerMovement>().haveNotJumpSameStar = false;
            other.gameObject.GetComponent<PlayerMovement>().canLand = true;
            other.gameObject.GetComponent<PlayerMovement>().onStar = false;
            other.gameObject.GetComponent<PlayerMovement>().onComet = false;
            other.gameObject.GetComponent<PlayerMovement>().onMeteor = false;
        }
    }
}
