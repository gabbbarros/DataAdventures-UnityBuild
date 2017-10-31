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

	/// <summary>
	/// Checks the fact count, and if we are above a new threshold, will play a new theme
	/// </summary>
	public void CheckFactCount()
	{
		float knownFactCount = 0;
		float totalFactCount = 0;
		foreach (Person p in FileReader.TheGameFile.people)
		{
			if (FileReader.TheGameFile.SearchSuspects(p.id) != null)
			{
				List<Fact> knownFacts = FileReader.TheGameFile.KnownFacts(p.id);
				knownFactCount += knownFacts.Count;
				List<Fact> totalFacts = FileReader.TheGameFile.SearchFacts(p.id);
				totalFactCount += totalFacts.Count;
			}
		}

		float factPercent = knownFactCount / totalFactCount;
		if (factPercent < ThresholdOne)
		{
			// TODO play theme 1
			if (currentThreshold != 1)
			{
				currentThreshold = 1;
				GM.SFXM.PlayLongTerm(8);
			}
		}
		else if (factPercent < ThresholdTwo)
		{
			// TODO play theme 2
			if (currentThreshold != 2)
			{
				currentThreshold = 2;
				GM.SFXM.PlayLongTerm(9);
			}
		}
		else if (factPercent < ThresholdThree)
		{
			// TODO play theme 3
			if (currentThreshold != 3)
			{
				currentThreshold = 3;
				GM.SFXM.PlayLongTerm(10);
			}
		}
		else if (factPercent < ThresholdFour)
		{
			// TODO play theme 4
			if (currentThreshold != 4)
			{
				currentThreshold = 4;
				GM.SFXM.PlayLongTerm(11);
			}
		}
		else
		{
			// TODO play finale theme
			if (currentThreshold != 5)
			{
				currentThreshold = 5;
				GM.SFXM.PlayLongTerm(12);
			}
		}

	}
}

