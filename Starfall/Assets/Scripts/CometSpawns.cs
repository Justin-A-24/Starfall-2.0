using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometSpawns : MonoBehaviour 
{
	public GameObject comet;
	public GameObject comet1;
	public GameObject comet2;
	public float nextSpawn = 0.0f;
	public float spawnRate = 1.0f;
	public float spawnLocationX = 91.0f;
	public float upperY = -4.0f;
	public float lowerY = -8.0f;
	public float randomY;
	public Vector2 spawnLocation;
	public GameObject cometsParent;
	public GameObject newComet;
	public CometMovements script;
	public int version;

	// Use this for initialization
	void Start () 
	{
		cometsParent = GameObject.FindGameObjectWithTag("Comets Parent");
		//script = gameObject.GetComponent<CometMovements>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		SpawnComet();
	}

	void SpawnComet()
	{
		if(Time.time > nextSpawn)
		{
			CometVersion();
			nextSpawn = Time.time + spawnRate;
			randomY = Random.Range(lowerY, upperY);
			spawnLocation = new Vector2(spawnLocationX, randomY);
			newComet = Instantiate(comet, spawnLocation, Quaternion.identity);
			newComet.transform.parent = cometsParent.transform;
			
		}
	}

	void CometVersion()
	{
		int randomNumber = Random.Range(0, 2);
		switch (randomNumber)
		{
			case 0:
			{
				comet = comet1;
				version = 1;
				//script.cometVersion = 1;
				break;
			}
			case 1:
			{
				comet = comet2;
				version = 2;
				//script.cometVersion = 2;
				break;
			}
			default:
			{
				comet = comet1;
				version = 1;
				//script.cometVersion = 1;
				break;
			}
		}
		script = comet.GetComponent<CometMovements>();
		script.cometVersion = version;
	}
}
