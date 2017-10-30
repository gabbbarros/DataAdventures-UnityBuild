using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroManager : MonoBehaviour
{
	public GameObject Fan;
	public Image Background;
	public IntroDialogueManager IDM;
	public SoundFXManager SFXM;
	public Animator EyeballAnimator;
	public Animator PhoneRingAnimator;
	public Animator FadeAnimator;
	public Animator MurderTextAnimator;
	public Animator MurderTextFlyManager;
	public GameObject SkipButton;


	public List<Sprite> Backgrounds;
	public void Start()
	{
		// make the intro happen
		EyeballAnimator.Play("EyeballWakeup");
		// start ringing
		PhoneRingAnimator.Play("RingRingAnimation");
		SFXM.PlaySingle(0);
	}

	public void AfterEyeballWakeup()
	{
		// start dialogue
		StartCoroutine(IntroDialogue1());
	}

	IEnumerator IntroDialogue1()
	{
		// AT HOME SECTION
		IDM.ShowBox("Me", "\'Mm... Deja vu...\'", 1, 1);//\n\nI better pick that up. It could be important.", 1, 1);
		yield return new WaitForSeconds(5);
		PhoneRingAnimator.Play("IdlePhone");
		IDM.ShowBox("Me:", "*pick up the phone* \n\n\"Hello?\"", 1, 1);
		yield return new WaitForSeconds(2);
		IDM.ShowBox("Operator:", "\"Agent, you are needed at the station. A code 616 has been issued.\"", 2, 2);
		yield return new WaitForSeconds(5);
		IDM.ShowBox("Me:", "<Crap!> \n\n\"Okay, I'm on my way!\" *hang up*", 1, 1);
		yield return new WaitForSeconds(2);
		IDM.HideBox(2);
		yield return new WaitForSeconds(3);
		IDM.ShowBox("Me:", "\'A code 616...? That can only mean one thing...", 1, 1);
		yield return new WaitForSeconds(3);
		//IDM.ShowBox("Me:", "MURDER", 1, 1);
		IDM.HideBox(1);
		SFXM.PlayLongTerm(1);
		// TODO show murder animation
		FadeAnimator.Play("Murder");
		MurderTextAnimator.Play("MurderText");
		MurderTextFlyManager.Play("MurderFly");
		yield return new WaitForSeconds(5);
		//fade to black while travelling to work
		FadeAnimator.Play("FadeOut");
		MurderTextAnimator.gameObject.SetActive(false);
		MurderTextFlyManager.gameObject.SetActive(false);
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
		IDM.ShowBox("Seargent:", "\"Agent, you are late...\nI trust you do remember why the " 
		            + "Inter-Dimensional Detective Agency exists and what it stands for?\"", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Me:", "Yes, Seargent. The IDDA is here to safeguard our universe from interdimensional paradoxes "
					+ "and time fallacies.", 1, 1);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Seargent:", "AND as a TIME-SENSITIVE Agency, I will remind you to not be late again, is that clear?", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Me:", "Understood loud and clear. What's the case I was called in for?", 1, 1);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Seargent:", "I think you are going to like this one. It's...unique...", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.HideBox(1);
		IDM.HideBox(2);
		FadeAnimator.gameObject.SetActive(false);
		// TODO : Now show the menu selection

	}

	public void SkipIntro()
	{
		IDM.HideBox(1);
		IDM.HideBox(2);

		StopAllCoroutines();
		Fan.SetActive(false);
		PhoneRingAnimator.gameObject.SetActive(false);
		EyeballAnimator.gameObject.SetActive(false);
		FadeAnimator.gameObject.SetActive(false);
		SFXM.soundJukebox.Stop();

		SkipButton.SetActive(false);
		SFXM.PlayLongTerm(1);
	}

	// TODO take an input to load the game file for this guy
	public void PlayGame()
	{
		SceneManager.LoadScene("WikiMystery");
	}

}

