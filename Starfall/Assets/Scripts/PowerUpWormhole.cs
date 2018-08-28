using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWormhole : MonoBehaviour
{
    public bool wormholeJumpable;
    public GameObject player;

    // Use this for initialization
    void Start () {
		player = GameObject.FindWithTag("Player");
        wormholeJumpable = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wormholeJumpable = false;
        }
    }
}
