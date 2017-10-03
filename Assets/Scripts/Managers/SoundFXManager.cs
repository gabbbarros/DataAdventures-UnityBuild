using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour {

	public List<AudioClip> sounds;
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
}
