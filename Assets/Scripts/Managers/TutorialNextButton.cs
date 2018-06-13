using System;
using UnityEngine;

public class TutorialNextButton : MonoBehaviour
{
	public GameObject img;
	public void Next()
	{
		img.SetActive(false);
	}
}

