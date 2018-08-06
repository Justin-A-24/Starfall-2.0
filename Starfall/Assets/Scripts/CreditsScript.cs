using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour 
{
	public List<CanvasGroup> canvasList;
	public CanvasGroup canvas0;
	public CanvasGroup canvas1;
	public CanvasGroup canvas2;
	public CanvasGroup canvas3;
	public CanvasGroup canvas4;
	public CanvasGroup canvas5;
	public CanvasGroup canvas6;
	public CanvasGroup canvas7;
	public int counter;
	public float canvasTime;
	public CanvasGroup currentCanvas;
	public bool fadingOut;
	// Use this for initialization
	void Start () 
	{
		fadingOut = false;
		canvasList = new List<CanvasGroup>(new CanvasGroup[]{canvas0, canvas1, canvas2, canvas3, canvas4, canvas5, canvas6, canvas7});
		counter = 0;
		canvasTime = 11.522f;
		foreach(CanvasGroup canvas in canvasList)
		{
			canvas.gameObject.SetActive(false);
			canvas.alpha = 0;
		}
		currentCanvas = canvasList[counter];
		canvasList[counter].gameObject.SetActive(true);
		FadeCreditsIn();
		Invoke("NextCanvas",canvasTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
	}

	void NextCanvas()
	{
		if(counter < canvasList.Count-1)
		{
			print(canvasList[counter]);
			canvasList[++counter].gameObject.SetActive(true);
			FadeCreditsIn();
			Invoke("NextCanvas", canvasTime);
		}
	}

	public void FadeCreditsIn()
	{
		StartCoroutine(FadeIn());
	}

	IEnumerator FadeIn()
	{
		CanvasGroup thisGroup = canvasList[counter].GetComponent<CanvasGroup>();
		while(thisGroup.alpha<1)
		{
			thisGroup.alpha += Time.deltaTime / 2;
			yield return null;
		}
		thisGroup.interactable = false;
		yield return null;
	}
}
