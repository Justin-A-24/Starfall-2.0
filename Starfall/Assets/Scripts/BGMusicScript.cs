using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicScript : MonoBehaviour 
{
	public AudioSource intro;
    public AudioSource loop;
	public AudioSource audioPlayer;

	// Use this for initialization
	void Start () 
	{
		audioPlayer = GetComponent<AudioSource>();
		audioPlayer = intro;
        audioPlayer.Play();
		audioPlayer = loop;
        audioPlayer.PlayDelayed(intro.clip.length);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

	}

}
