using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Invoke("NextScene", 12f);
	}
	
	void NextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
	}
}
