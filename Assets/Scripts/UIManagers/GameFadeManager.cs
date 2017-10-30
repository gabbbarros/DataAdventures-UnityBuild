using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFadeManager : MonoBehaviour {

	public GameManager GM;
	public void CanIStartOver()
	{
		GM.ReturnToIntroScene();
	}
}
