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
	public SoundFXManager SFXM;
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
		IDM.ShowBox("Me:", "\'Mm... Deja vu...\'", 1, 1);//\n\nI better pick that up. It could be important.", 1, 1);
		yield return new WaitForSeconds(5);
		PhoneRingAnimator.Play("IdlePhone");
		IDM.ShowBox("Me:", "*pick up the phone* \n\n\"Hello?\"", 1, 1);
		SFXM.PlayLongTerm(1);
		yield return new WaitForSeconds(2);
		IDM.ShowBox("Operator:", "\"Agent, you are needed at the station. A code 616 has been issued.\"", 2, 2);
		yield return new WaitForSeconds(5);
		IDM.ShowBox("Me:", "<Crap!> \n\n\"Okay, I'm on my way!\" *hang up*", 1, 1);
		yield return new WaitForSeconds(2);
		IDM.HideBox(2);
		yield return new WaitForSeconds(3);
		IDM.ShowBox("Me:", "\'A code 616...? That can only mean one thing...\n\n...Murder\'", 1, 1);
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
		IDM.ShowBox("Sergent:", "\"Agent, you are late.\nThis is a is a time agency. Tardiness will not be tolerated.\"", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Me:", "*sigh* Yes, Sergent.", 1, 1);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Sergent:", "While you were away, multiple anomalies have been spotted. "+
			"People are being murdered and the culprits are disguising themselves as regular people.", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Sergent:", "These anomalies have corrupted our information system, and we cannot identify who are the culprits. "+
			"However, we can identify a few people of interest.", 2, 2);
			yield return new WaitForSeconds(4);
		IDM.ShowBox("Sergent:", "Investigate these people's lives. The culprit is disguised, "+
			"but they do not possess all information about the person they are impersonating.", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Me:", "So find inconsistencies, right? Understood loud and clear.", 1, 1);
		yield return new WaitForSeconds(4);
		IDM.ShowBox("Sergent:", "Good luck, agent.", 2, 2);
		yield return new WaitForSeconds(4);
		IDM.HideBox(1);
		IDM.HideBox(2);
		// TODO : Now show the menu selection

	}

}

