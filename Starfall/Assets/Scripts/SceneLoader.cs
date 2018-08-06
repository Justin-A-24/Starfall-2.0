using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour 
{
	public GameObject loadingScreenObj;
	public Slider loadingSlider;
	AsyncOperation async;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void DisableMainMenu()
	{
		GameObject.FindGameObjectWithTag("Main Menu Canvas").SetActive(false);
	}

	public void LoadScreen(int level)
	{
		StartCoroutine(LoadingScreen(level));
	}

	IEnumerator LoadingScreen(int level)
	{
		loadingScreenObj.SetActive(true);
		async = SceneManager.LoadSceneAsync(level);
		async.allowSceneActivation = false;
		

		while(!async.isDone)
		{
			loadingSlider.value = async.progress;
			//print(loadingSlider.value);
			//yield return new WaitForSeconds(5);
			yield return new WaitForSecondsRealtime(3);
			if(async.progress == 0.9f)
			{
				loadingSlider.value = 1.0f;
				async.allowSceneActivation = true;
			}
			yield return null;
		}

	}
}
