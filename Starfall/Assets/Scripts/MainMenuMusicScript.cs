using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicScript : MonoBehaviour 
{
	public AudioSource clip1;
    public AudioSource clip2;
	public AudioSource clip3;
	public AudioSource audioPlayer;
	public bool musicDone;

	// Use this for initialization
	void Start () 
	{
		PlayBGMusic();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(musicDone)
		{
			PlayBGMusic();
		}
	}

	void PlayBGMusic()
	{
		musicDone = false;
		audioPlayer = GetComponent<AudioSource>();
		audioPlayer = clip1;
        audioPlayer.Play();
		audioPlayer = clip2;
        audioPlayer.PlayDelayed(clip1.clip.length);
		audioPlayer = clip3;
        audioPlayer.PlayDelayed(clip1.clip.length + clip2.clip.length);
		Invoke("ResetBool", clip1.clip.length + clip2.clip.length + clip3.clip.length);
	}

	void ResetBool()
	{
		musicDone = true;	
	}
}
