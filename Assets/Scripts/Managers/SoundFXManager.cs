using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour {

	public List<AudioClip> sounds;
	public List<AudioClip> dooropen;
	public List<AudioClip> doorshut;
	public AudioSource soundJukebox;
	public static SoundFXManager instance = null;

	void Awake()
	{
		// check to see if an instance exists
		if (instance == null)
		{
			// if not set it to this
			instance = this;
		}
	}
	public void PlaySingle(int clipID)
	{
		// set clip to whatever we want to play
		soundJukebox.clip = sounds[clipID];

		// play clip
		soundJukebox.Play();
	}

	public void PlaySingle(string type)
	{
		// set clip to whatever we want to play
		if (type.Equals("dooropen"))
		{
			soundJukebox.clip = dooropen[Random.Range(0, dooropen.Count)];
		}
		else if (type.Equals("doorshut")) 
		{ 
			soundJukebox.clip = doorshut[Random.Range(0, doorshut.Count)];
		}
		// play clip
		soundJukebox.Play();
	}
}
