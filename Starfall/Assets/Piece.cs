using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    public GameObject endless_piece;
    public int bg_index;

    [SerializeField]
    private bool canSpawn;

    [SerializeField]
    private GameObject[] background;

    public void Start()
    {
        canSpawn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks to see if the player has cross the collider in order to 
        // spawn a new piece
        if (collision.CompareTag("Player") && canSpawn)
        {
            canSpawn = false;
            Spawn();
        }
    }

    public void ChangeBG()
    {
        background[bg_index].SetActive(true);
    }

    // Spawns a new piece to the endless level
    public void Spawn()
    {
        var piece = Instantiate(endless_piece, transform.position, Quaternion.identity);
        Piece spawned_piece = piece.GetComponentInChildren<Piece>();
        spawned_piece.bg_index = bg_index;
        spawned_piece.ChangeBG();
    }
}
