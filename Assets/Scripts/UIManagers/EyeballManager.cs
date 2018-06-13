using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballManager : MonoBehaviour {

	public IntroManager IM;

	public void StopBlinkingPlease() 
	{
		IM.AfterEyeballWakeup();
	}
}
