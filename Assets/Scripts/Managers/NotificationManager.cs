using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

    public GameManager GM;

	public Animator JournalTabAnimator;
	public Animator PeopleTabAnimator;
	public Animator PlacesTabAnimator;

	public Animator NotificationDude;
	public GameObject NotificationProfileImage;
	public Text NotificationText;


	public Sprite Exclaim;
	public void ShowPeopleNotification()
	{
        // play animations if it's the right time
        if (GM.OverallFocus != 2)
            JournalTabAnimator.Play("NewJournalAnimation");
        if(GM.JournalTabFocus != 1)
            PeopleTabAnimator.Play("NewJournalAnimation");
	}

	public void ShowPlacesNotification()
	{
		// play animations if it's the right time
        if (GM.OverallFocus != 2)
            JournalTabAnimator.Play("NewJournalAnimation");
        if(GM.JournalTabFocus != 2)
            PlacesTabAnimator.Play("NewJournalAnimation");
	}

	public void ShowThingsNotification()
	{
		// play animations if it's the right time
		if (GM.OverallFocus != 2)
            JournalTabAnimator.Play("NewJournalAnimation");
        if(GM.JournalTabFocus != 3)
            PlacesTabAnimator.Play("NewJournalAnimation");
	}

	public void ShowNewFactNotification(Person me)
	{
		NotificationText.text = "New Fact Revealed!";
		Sprite myFace = GM.SearchPeopleSprites(me.image);

		// change the face
		NotificationProfileImage.GetComponent<Image>().overrideSprite = myFace;
		NotificationDude.Play("NotifyPlayer");
	}

	public void ShowNewClueNotification()
	{
		NotificationText.text = "New Clue Discovered!";
		NotificationProfileImage.GetComponent<Image>().overrideSprite = Exclaim;

		// Show animation
		NotificationDude.Play("NotifyPlayer");
	}
}
