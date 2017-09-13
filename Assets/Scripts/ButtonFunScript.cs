using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunScript : MonoBehaviour {

	public GameObject Light;

	public void Pressed()
	{
		Light.SetActive(!Light.active);
	}
}
