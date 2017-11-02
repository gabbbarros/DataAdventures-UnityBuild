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

	public int dialogueAdvance;

	public List<Sprite> Backgrounds;
	public void Start()
	{
		// make the intro happen
		Fan.SetActive(true);
		EyeballAnimator.Play("EyeballWakeup");
		FadeAnimator.gameObject.GetComponent<Button>().enabled = false;
		//MurderTextAnimator.gameObject.GetComponent<Button>().enabled = false;
		// start ringing
		PhoneRingAnimator.Play("RingRingAnimation");
		SFXM.PlaySingle(0);
		StartCoroutine(LoadGames());
	}

	public void AfterEyeballWakeup()
	{
		// start dialogue
		//StartCoroutine(IntroDialogue1());
		IntroDialogueAdvance();
		FadeAnimator.gameObject.GetComponent<Button>().enabled = true;
		//MurderTextAnimator.gameObject.GetComponent<Button>().enabled = false;
	}

	public void IntroDialogueAdvance()
	{
		if (dialogueAdvance == 0)
		{
			IDM.ShowBox("Me", "\'Mm...wha...\nDeja vu...\'", 1, 1);
		}
		else if (dialogueAdvance == 1)
		{
			PhoneRingAnimator.Play("IdlePhone");
			IDM.ShowBox("Me:", "*pick up the phone* \n\n\"Hello?\"", 1, 1);
		}
		else if (dialogueAdvance == 2)
		{
			IDM.ShowBox("Operator:", "\"Agent, you are needed at the station. A code 616 has been issued.\"", 2, 2);
		}
		else if (dialogueAdvance == 3)
		{
			IDM.ShowBox("Me:", "<Crap!> \n\n\"Okay, I'm on my way!\" *hang up*", 1, 1);
		}
		else if (dialogueAdvance == 4)
		{
			IDM.HideBox(2);
			IDM.ShowBox("Me:", "\'A code 616...? That can only mean one thing...\'", 1, 1);
		}
		else if (dialogueAdvance == 5)
		{
			MurderTextAnimator.gameObject.SetActive(true);
			MurderTextFlyManager.gameObject.SetActive(true);
			IDM.HideBox(1);
			SFXM.PlayLongTerm(1);
			// TODO show murder animation
			FadeAnimator.Play("Murder");
			MurderTextAnimator.Play("MurderText");
			MurderTextFlyManager.Play("MurderFly");
		}
		else if (dialogueAdvance == 6)
		{
			//fade to black while travelling to work
			FadeAnimator.gameObject.GetComponent<Button>().enabled = false;
			MurderTextAnimator.gameObject.GetComponent<Button>().enabled = false;
			FadeAnimator.Play("FadeOut");
			MurderTextAnimator.gameObject.SetActive(false);
			MurderTextFlyManager.gameObject.SetActive(false);
		}
		else if (dialogueAdvance == 7)
		{
			IDM.ShowBox("Sergent:", "\"Agent, you are late.\nThis is a is a time agency. Tardiness will not be tolerated.\"", 2, 2);
			FadeAnimator.gameObject.GetComponent<Button>().enabled = true;
		}
		else if (dialogueAdvance == 8)
		{
			IDM.ShowBox("Me:", "*sigh* Yes, Sergent.", 1, 1);
		}
		else if (dialogueAdvance == 9)
		{
			IDM.ShowBox("Sergent:", "While you were away, multiple anomalies have been spotted. "+
			"People are being murdered and the culprits are disguising themselves as regular people.", 2, 2);
		}
		else if (dialogueAdvance == 10)
		{
			IDM.ShowBox("Sergent:", "These anomalies have corrupted our information system, and we cannot identify who are the culprits. "+
			"However, we can identify a few people of interest.", 2, 2);
			yield return new WaitForSeconds(4);
		}
		else if (dialogueAdvance == 11)
		{
			IDM.ShowBox("Sergent:", "Investigate these people's lives. The culprit is disguised, "+
			"but they do not possess all information about the person they are impersonating.", 2, 2);
		}
		else if (dialogueAdvance == 12)
		{
			IDM.ShowBox("Me:", "So find inconsistencies, right? Understood loud and clear.", 1, 1);
		}else if (dialogueAdvance == 13)
		{
			IDM.ShowBox("Sergent:", "Good luck, agent.", 2, 2);
		}else if (dialogueAdvance == 14)
		{
			IDM.HideBox(1);
			IDM.HideBox(2);
			FadeAnimator.gameObject.SetActive(false);
			SkipButton.SetActive(false);
		}
		Debug.Log(dialogueAdvance);
		dialogueAdvance++;
	}



	public void ChangeToOffice()
	{
		// hide dialogue
		IDM.HideBox(1);
		// disable fan
		Fan.SetActive(false);
		// change background
		//Background.overrideSprite = Backgrounds[0];

		//once everything has been swapped, fade in
		FadeAnimator.Play("FadeIn");
		IntroDialogueAdvance();
		//StartCoroutine(IntroDialogue2());
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
		MurderTextAnimator.gameObject.SetActive(false);
		MurderTextFlyManager.gameObject.SetActive(false);
	}

	// TODO take an input to load the game file for this guy
	public void PlayGame()
	{
		SceneManager.LoadScene("WikiMystery");
	}

	/// <summary>
	/// Loads the games.
	/// </summary>
	/// <returns>The games.</returns>
	IEnumerator LoadGames()
	{
		
		yield return null;
	}

}

