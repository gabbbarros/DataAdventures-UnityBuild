using System;
using UnityEngine;
using UnityEngine.UI;
public class IntroDialogueManager : MonoBehaviour
{
	public GameObject DBox1;
	public GameObject DBox2;
	public Text DText1;
	public Text DText2;

	public Text DTextName1;
	public Text DTextName2;

	public Animator DBoxAnimator1;
	public Animator DBoxAnimator2;
	// Use this for initialization
	void Start()
	{
	}

	/**
	 * Shows the dialogue box with the character saying the text in this format
	 * [Character] : [text]
	 */
	public void ShowBox(string character, string text, int numDBox, int animNum)
	{
        Debug.Log("Show box: " + character + " : " + text);
		GameObject DBox;
		Text DText, DTextName;
		Animator DBoxAnimator;
		if (numDBox == 1)
		{
			DBox = DBox1;
			DText = DText1;
			DTextName = DTextName1;
			DBoxAnimator = DBoxAnimator1;
		}
		else
		{
			DBox = DBox2;
			DText = DText2;
			DTextName = DTextName2;
			DBoxAnimator = DBoxAnimator2;
		}

		DText.text = text;
		DTextName.text = character;
		DBox.SetActive(true);

		if (animNum == 1)
		{
			//TODO play animation one
			DBoxAnimator.Play("DialogueAnimationEntrance1");
		}
		else if (animNum == 2)
		{
			//TODO play animation two
			DBoxAnimator.Play("DialogueAnimationEntrance2");
		}
		else if (animNum == 3)
		{
			//TODO play animation three
			DBoxAnimator.Play("DialogueAnimationEntrance3");
		}
	}

	public void HideBox(int numDBox)
	{
		Animator DBoxAnimator;
		if (numDBox == 1)
		{
			DBoxAnimator = DBoxAnimator1;
		}
		else
		{
			DBoxAnimator = DBoxAnimator2;
		}

		DBoxAnimator.Play("IdleDialogue");
		//TODO smooth fade?
	}
}

