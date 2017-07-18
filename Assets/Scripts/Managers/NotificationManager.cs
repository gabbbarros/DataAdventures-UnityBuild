using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

	public Animator JournalTabAnimator;
	public Animator PeopleTabAnimator;
	public Animator PlacesTabAnimator;

	public void ShowPeopleNotification()
	{
		JournalTabAnimator.Play("NewJournalAnimation");
	}

	public void ShowPlacesNotification()
	{

	}
}
