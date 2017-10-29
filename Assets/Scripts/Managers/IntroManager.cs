using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IntroManager : MonoBehaviour
{
	public GameObject Fan;
	public Image Background;
	public IntroDialogueManager IDM;
	public Animator EyeballAnimator;
	public Animator PhoneRingAnimator;
	public Animator FadeAnimator;

	public List<Sprite> Backgrounds;
	public void Start()
	{
		// make the intro happen
		EyeballAnimator.Play("EyeballWakeup");
		// start ringing
		PhoneRingAnimator.Play("RingRingAnimation");
	}

	public void AfterEyeballWakeup()
	{
		// start dialogue
		StartCoroutine(IntroDialogue1());
	}

	IEnumerator IntroDialogue1()
	{
		// AT HOME SECTION
		IDM.ShowBox("Me", "I better pick that up. It could be important.", 1, 1);
		yield return new WaitForSeconds(5);
		PhoneRingAnimator.Play("IdlePhone");
		IDM.ShowBox("Me:", "*pick up the phone* \nHello?", 1, 1);
		yield return new WaitForSeconds(2);
		IDM.ShowBox("Operator:", "Agent, you are needed at the station. A code 616 has been issued.", 2, 2);
		yield return new WaitForSeconds(5);
		IDM.ShowBox("Me:", "Alright, I'll be there in twenty... *hang up*", 1, 1);
		yield return new WaitForSeconds(2);
		IDM.HideBox(2);
		yield return new WaitForSeconds(3);
		IDM.ShowBox("Me:", "A code 616...? That can only mean one thing...Murder", 1, 1);
		yield return new WaitForSeconds(5);
		//fade to black while travelling to work
		FadeAnimator.Play("FadeOut");
		//TODO play some sort of travel sound here

		//TODO change scene to office background
	}


	public void ChangeToOffice()
	{
		// hide dialogue
		IDM.HideBox(1);
		// disable fan
		Fan.SetActive(false);
		// change background
		Background.overrideSprite = Backgrounds[0];

		//once everything has been swapped, fade in
		FadeAnimator.Play("FadeIn");
		StartCoroutine(IntroDialogue2());
	}

	IEnumerator IntroDialogue2()
	{
		yield return new WaitForSeconds(3);
		IDM.ShowBox("Seargent RobotFace:", "Special Agent, you are late...\n I trust you do remember what the " 
		            + "Inter-Dimensional Detective Agency is for and what it stands for?", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Me:", "Yes, Seargent. The IDDA is here to safeguard our universe from interdimensional paradoxes "
					+ "and time fallacies.", 1, 1);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Seargent RobotFace:", "AND as a TIME-SENSITIVE Agency, I will remind you to not be late again, is that clear?", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Me:", "Understood loud and clear. What's the case I was called in for?", 1, 1);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Seargent RobotFace:", "I think you are going to like this one. It's...unique...", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.HideBox(1);
		IDM.HideBox(2);
		// TODO : Now show the menu selection

	}

}

