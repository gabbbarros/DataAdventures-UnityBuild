using System;
using UnityEngine;
public class IntroFadeManager : MonoBehaviour
{
	public IntroManager IM;
	public void CanIGoToWorkNow()
	{
		IM.ChangeToOffice();
	}
}

