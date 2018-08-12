using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject endless_piece;

    [SerializeField]
    private bool canSpawn;

	// Use this for initialization
	void Start () {
        //Spawn();
        canSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canSpawn) {
            canSpawn = false;
            Spawn();
        }
    }

    public void Spawn() {
        Instantiate(endless_piece, transform.position, Quaternion.identity);
    }
}
