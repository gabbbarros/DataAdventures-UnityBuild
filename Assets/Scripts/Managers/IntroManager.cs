using System;
using System.Collections;
using UnityEngine;
public class IntroManager : MonoBehaviour
{
	public IntroDialogueManager IDM;
	public Animator EyeballAnimator;
	public Animator PhoneRingAnimator;

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
		StartCoroutine(DelayDialogue(5));
	}

	IEnumerator DelayDialogue(int seconds)
	{
		// AT HOME SECTION
		IDM.ShowBox("Me", "I better pick that up. It could be important.", 1, 1);
		yield return new WaitForSeconds(seconds);
		PhoneRingAnimator.Play("IdlePhone");
		IDM.ShowBox("Me:", "*pick up the phone* \nHello?", 1, 1);
		yield return new WaitForSeconds(2);
		IDM.ShowBox("Operator:", "Agent, you are needed at the station. A code 616 has been issued.", 2, 2);
		yield return new WaitForSeconds(5);
		IDM.ShowBox("Me:", "Alright, I'll be there in twenty... *hang up*", 1, 1);
		yield return new WaitForSeconds(5);
		IDM.ShowBox("Me:", "A code 616...? That can only mean one thing...Murder", 1, 1);

		//TODO fade to black while travelling to work

		//
	}

}

