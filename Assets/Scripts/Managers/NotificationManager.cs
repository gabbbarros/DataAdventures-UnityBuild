using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    public GameManager GM;

	public Animator JournalTabAnimator;
	public Animator PeopleTabAnimator;
	public Animator PlacesTabAnimator;

	public void ShowPeopleNotification()
	{
        // play animations if its the right time
        if (GM.OverallFocus != 2)
            JournalTabAnimator.Play("NewJournalAnimation");
        if(GM.JournalTabFocus != 1)
            PeopleTabAnimator.Play("NewJournalAnimation");
	}

	public void ShowPlacesNotification()
	{

	}
}
