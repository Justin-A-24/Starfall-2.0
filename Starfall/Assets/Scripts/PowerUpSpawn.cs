using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Most of the code is copy from script StarSpawns
public class PowerUpSpawn : MonoBehaviour {
    public GameObject powerUp;
    public GameObject powerUpFuel;
    public GameObject powerUpShield;
    public GameObject powerUpWormhole;
    public float nextSpawn = 0.0f;
    public float spawnRate = 1.0f;
    public float spawnLocationX = 91.0f;
    public float upperY = -4.0f;
    public float lowerY = -8.0f;
    public float randomY;
    public Vector2 spawnLocation;
    public GameObject powerUpParent;
    public GameObject newPowerUp;

    // Use this for initialization
    void Start () {
        powerUpParent = GameObject.FindGameObjectWithTag("Power Up Parent");
    }
	
	// Update is called once per frame
	void Update ()
	{
	    SpawnPowerUp();
	}

    void SpawnPowerUp()
    {
        if (Time.time > nextSpawn)
        {
            PowerUpColor();
            nextSpawn = Time.time + spawnRate;
            randomY = Random.Range(lowerY, upperY);
            spawnLocation = new Vector2(spawnLocationX, randomY);
            newPowerUp = Instantiate(powerUp, spawnLocation, Quaternion.identity);
            newPowerUp.transform.parent = powerUpParent.transform;
        }
    }

    void PowerUpColor()
    {
        int randomNumber = Random.Range(0, 3);
        switch (randomNumber)
        {
            case 0:
                powerUp = powerUpFuel;
                break;
            case 1:
                powerUp = powerUpShield;
                break;
            case 2:
                //No want use, rather add "To do list for random x spawn". This powerUp does not move as it should so the spawn would only be in one x location
                //powerUp = powerUpWormhole;
                Debug.Log("Have not set up random spawn for powerUpWormhole, check PowerUpSpawn function PowerUpColor");
                break;
            default:
                powerUp = powerUpFuel;
                break;
        }
    }
}
