using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunScript : MonoBehaviour {

	public GameObject Light;
	public GameManager GM;

	void Start()
	{
		GM = GameObject.FindWithTag("UI Manager").GetComponent<GameManager>();
	}

	public void Pressed()
	{
		if (Light.active)
		{
			GM.PlaySoundFX(4);
		}
		else
		{
			GM.PlaySoundFX(3);	
		}
		Light.SetActive(!Light.active);
	}
}
