using System;
using UnityEngine;
using System.Collections.Generic;
public class TutorialManager : MonoBehaviour
{
	// Future me, please forgive past me. (or present me? idk as I write this I suppose it's the present)
	// This is a really sloppy implementation, but know that you had a deadline this time of the year
	// and it's a quick and dirty way of making that deadline a possibility. 
	// Cheers, Past (present?) me.

	/// <summary>
	/// The imgs.
	/// </summary>
	public List<GameObject> imgs;
	public void RestartTutorial()
	{
		foreach (GameObject img in imgs)
		{
			img.SetActive(true);
		}
	}
}

