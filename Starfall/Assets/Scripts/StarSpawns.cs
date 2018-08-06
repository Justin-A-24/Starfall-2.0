using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawns : MonoBehaviour 
{
	public GameObject star;
	public GameObject whiteStar;
	public GameObject pinkStar;
	public GameObject purpleStar;
	public float nextSpawn = 0.0f;
	public float spawnRate = 1.0f;
	public float spawnLocationX = 91.0f;
	public float upperY = -4.0f;
	public float lowerY = -8.0f;
	public float randomY;
	public Vector2 spawnLocation;
	public GameObject starsParent;
	public GameObject newStar;

	// Use this for initialization
	void Start () 
	{
		starsParent = GameObject.FindGameObjectWithTag("Stars Parent");
	}

	// Update is called once per frame
	void Update () 
	{
		SpawnStar();
	}

	void SpawnStar()
	{
		if(Time.time > nextSpawn)
		{
			StarColor();
			nextSpawn = Time.time + spawnRate;
			randomY = Random.Range(lowerY, upperY);
			spawnLocation = new Vector2(spawnLocationX, randomY);
			newStar = Instantiate(star, spawnLocation, Quaternion.identity);
			newStar.transform.parent = starsParent.transform;
		}
	}

	void StarColor()
	{
		int randomNumber = Random.Range(0, 3);
		switch (randomNumber)
		{
			case 0:
				star = purpleStar;
				break;
			case 1:
				star = whiteStar;
				break;
			case 2:
				star = pinkStar;
				break;
			default:
				star = purpleStar;
				break;
		}
		
	}
}
