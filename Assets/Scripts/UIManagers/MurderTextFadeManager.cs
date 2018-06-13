using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderTextFadeManager : MonoBehaviour {

	public IntroManager IM;
	public void MayIFade()
	{
		IM.IntroDialogueAdvance();
	}
}
