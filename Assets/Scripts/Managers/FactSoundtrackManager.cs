using System;
using System.Collections.Generic;
using UnityEngine;
public class FactSoundtrackManager : MonoBehaviour
{
	public GameManager GM;
	public float ThresholdOne;
	public float ThresholdTwo;
	public float ThresholdThree;
	public float ThresholdFour;


	public int currentThreshold;

	private int[] factsKnownCounter = null;

	/// <summary>
	/// Checks the fact count, and if we are above a new threshold, will play a new theme
	/// </summary>
	public Person CheckFactCount()
	{
		if (factsKnownCounter == null)
		{
			factsKnownCounter = new int[FileReader.TheGameFile.GetSuspectCount()];
		}
		Person changed = null;
		float knownFactCount = 0;
		float totalFactCount = 0;
		int counter = 0;
		foreach (Suspect s in FileReader.TheGameFile.Suspects)
		{

			//Debug.Log(s.me.name);
			List<Fact> knownFacts = FileReader.TheGameFile.KnownFacts(s.me.id);
			knownFactCount += knownFacts.Count;
			List<Fact> totalFacts = FileReader.TheGameFile.SearchFacts(s.me.id);
			totalFactCount += totalFacts.Count;
			// check to see if we changed here
			if (factsKnownCounter[counter] < knownFacts.Count)
			{
				changed = s.me;
				factsKnownCounter[counter] = knownFacts.Count;
			}
			counter++;

		}

		float factPercent = knownFactCount / totalFactCount;
		if (factPercent < ThresholdOne)
		{
			// TODO play theme 1
			if (currentThreshold != 1)
			{
				currentThreshold = 1;
				GM.SFXM.PlayLongTerm(8, 1.0f);
			}
		}
		else if (factPercent < ThresholdTwo)
		{
			// TODO play theme 2
			if (currentThreshold != 2)
			{
				currentThreshold = 2;
				GM.SFXM.PlayLongTerm(9, 1.0f);
			}
		}
		else if (factPercent < ThresholdThree)
		{
			// TODO play theme 3
			if (currentThreshold != 3)
			{
				currentThreshold = 3;
				GM.SFXM.PlayLongTerm(10, 0.5f);
			}
		}
		else if (factPercent < ThresholdFour)
		{
			// TODO play theme 4
			if (currentThreshold != 4)
			{
				currentThreshold = 4;
				GM.SFXM.PlayLongTerm(11, 0.5f);
			}
		}
		else
		{
			// TODO play finale theme
			if (currentThreshold != 5)
			{
				currentThreshold = 5;
				GM.SFXM.PlayLongTerm(12, 0.5f);
			}
		}
		return changed;
	}
}

