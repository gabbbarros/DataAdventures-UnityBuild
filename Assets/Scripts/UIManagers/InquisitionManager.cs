using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InquisitionManager : MonoBehaviour
{
	public Person s;

	public GameManager GM;

	public Text Name;
	public Animator InquisitionAnimator;
	public GameObject Dialogue;

	public GameObject TrueChoice;
	public GameObject FalseChoice;

	public GameObject ArrestPanel;
	public GameObject Image;


	public Building Location;

	public List<String> FactList;
	public int[] checker;


	public int Counter;
	/// <summary>
	/// Initialize an inquisition
	/// </summary>
	public void Initialize(Person me)
	{
		// make arrest panel disappear
		ArrestPanel.SetActive(false);
		// Set person and name
		s = me;
		Name.text = s.name;
		// TODO set image
		Sprite myFace = GM.SearchPeopleSprites(me.image);

		Image.GetComponent<Image>().overrideSprite = myFace;
		// Go through this person's dialogue tree and find all the 626 node ID's
		FactList = BFS();

		// Set first dialogue Line, play animation
		Dialogue.GetComponent<Text>().text = "You found me. Let me tell you about myself and prove my innocence.";
		InquisitionAnimator.Play("Start");

		// load up person facts
		//FactList = FileReader.TheGameFile.SearchFacts(me.id);
		checker = new int[FactList.Count];

		for (int i = 0; i < checker.Length; i++)
		{
			checker[i] = -1;
		}

		// Show on the description screen
		// TODO scramble facts
	}

	/// <summary>
	/// Displays the next fact.
	/// </summary>
	public void DisplayFact()
	{
		// check to see if we are done checking
		//bool checkChecker = true;
		//for (int i = 0; i < checker.Length; i++)
		//{
		//	if (checker[i] == -1)
		//	{
		//		checkChecker = false;
		//		break;
		//	}
		//}
		Debug.Log(FactList.Count);
		Counter++;
		if (Counter >= FactList.Count)
		{
			Counter = 0;
		}
		InquisitionAnimator.Play("Fly1");
		Dialogue.GetComponent<Text>().text = FactList[Counter];
		TrueChoice.GetComponent<Button>().enabled = true;
		FalseChoice.GetComponent<Button>().enabled = true;
		//StartCoroutine(WaitForPlayerChoice());

		//else
		//{
		//	Location = FileReader.TheGameFile.SearchBuildings(s.buildingid);
		//	GM.TravelHere(Location);
		//	// TODO display some sort of end to this
		//}
	}

	/// <summary>
	/// The player said this fact is true.
	/// </summary>
	public void ChooseTrue()
	{
		TrueChoice.GetComponent<Button>().enabled = false;
		StopAllCoroutines();
		checker[Counter] = 1;
		GM.DM.SetFacts(s);
		DisplayFact();
	}

	/// <summary>
	/// The player said this fact is false.
	/// </summary>
	public void ChooseFalse()
	{
		FalseChoice.GetComponent<Button>().enabled = true;
		StopAllCoroutines();
		checker[Counter] = 0;
		GM.DM.SetFacts(s);
		ShowArrestChoice();
		//DisplayFact();
		// TODO mark this in your journal?
	}
	/// <summary>
	/// Asks the user if they would like to make an arrest based off the false data
	/// </summary>
	public void ShowArrestChoice()
	{
		ArrestPanel.SetActive(true);
	}

	/// <summary>
	/// Makes the arrest.
	/// </summary>
	public void MakeArrest()
	{
		GM.ShowArrestScreen(s, FileReader.TheGameFile.crime.culprit == s.id);
	}

	/// <summary>
	/// Takes the player back to displaying facts to "speculate" more
	/// </summary>
	public void Speculate()
	{
		ArrestPanel.SetActive(false);
		DisplayFact();
	}
	IEnumerator WaitForPlayerChoice()
	{
		yield return new WaitForSeconds(5.0f);
		DisplayFact();
	}

	public List<String> BFS()
	{
		List<String> myFacts = new List<String>();
		List<DialogueNode> explored = new List<DialogueNode>();
		List<DialogueNode> frontier = new List<DialogueNode>();
		frontier.Add(s.rootnode);
		// while the frontier is not empty
		while (frontier.Count > 0)
		{
			DialogueNode current = frontier[0];
			frontier.RemoveAt(0);
			explored.Add(current);

			foreach (DialogueNode child in current.childrenNodes)
			{
				if (!explored.Contains(child))
				{
					frontier.Add(child);
				}
			}

			if (current.dialoguetype == 626)
			{
				myFacts.Add(current.dialogueline);
			}
		}
		return myFacts;
	}

	public void Leave()
	{
		Location = FileReader.TheGameFile.SearchBuildings(s.buildingid);
		GM.TravelHere(Location);
	}

	public void StopEverything()
	{
		StopAllCoroutines();
	}
}
