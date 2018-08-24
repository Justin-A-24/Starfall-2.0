using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public GameObject endless_piece;

    [SerializeField]
    private bool canSpawn;

    [SerializeField]
    private GameObject[] background;

    //[SerializeField]
    public int bg_index;

	// Use this for initialization
	void Start () {
        canSpawn = true;

        // Chooses a random background and sets it to active
        bg_index = Random.Range(0, background.Length);
        background[bg_index].SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks to see if the player has cross the collider in order to 
        // spawn a new piece
        if (collision.CompareTag("Player") && canSpawn) {
            canSpawn = false;
            Spawn();
        }
    }

    // Spawns a new piece to the endless level
    public void Spawn() {
        var piece = Instantiate(endless_piece, transform.position, Quaternion.identity);
        Piece spawned_piece = piece.GetComponentInChildren<Piece>();
        spawned_piece.bg_index = bg_index;
        spawned_piece.ChangeBG();
    }
}
