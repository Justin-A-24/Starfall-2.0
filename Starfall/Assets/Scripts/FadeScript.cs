using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour 
{
	public float fadeSpeed = 1.5f;
	private bool sceneStarting = true;
	public GUITexture myTexture;

	void Awake()
	{
		myTexture = gameObject.GetComponent<GUITexture>();
		myTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);

	}

	void Update()
	{
		if(sceneStarting)
			StartScreen();
	}

	void FadeToClear()
	{
		myTexture.color = Color.Lerp(myTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack()
	{
		myTexture.color = Color.Lerp(myTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	void StartScreen()
	{
		FadeToClear();
		if(myTexture.color.a <= 0.05f)
		{
			myTexture.color = Color.clear;
			myTexture.enabled = false;
			sceneStarting = false;
		}
	}

	public void EndScene()
	{
		myTexture.enabled = true;
		FadeToBlack();
		if(myTexture.color.a >= 0.95f)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
		}
	}
}
