using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawns : MonoBehaviour 
{
	public GameObject meteor;
	public GameObject meteor1;
	public GameObject meteor2;
	public float nextSpawn = 0.0f;
	public float spawnRate = 1.0f;
	public float spawnLocationX = 91.0f;
	public float upperY = -4.0f;
	public float lowerY = -8.0f;
	public float randomY;
	public Vector2 spawnLocation;
	public GameObject meteorsParent;
	public GameObject newMeteor;
	// Use this for initialization
	void Start () 
	{
		meteorsParent = GameObject.FindGameObjectWithTag("Meteors Parent");
	}
	
	// Update is called once per frame
	void Update () 
	{
		SpawnMeteor();
	}

	void SpawnMeteor()
	{
		if(Time.time > nextSpawn)
		{
			MeteorVersion();
			nextSpawn = Time.time + spawnRate;
			randomY = Random.Range(lowerY, upperY);
			spawnLocation = new Vector2(spawnLocationX, randomY);
			newMeteor = Instantiate(meteor, spawnLocation, Quaternion.identity);
			newMeteor.transform.parent = meteorsParent.transform;
		}
	}

	void MeteorVersion()
	{
		int randomNumber = Random.Range(0, 2);
		switch (randomNumber)
		{
			case 0:
				meteor = meteor1;
				break;
			case 1:
				meteor = meteor2;
				break;
			default:
				meteor = meteor1;
				break;
		}
	}
}
