﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour {

	public GameManager GM;

	public GameObject PeoplePanelContent;
	public GameObject PlacesPanelContent;
	public GameObject ThingsPanelContent;

	public GameObject PeopleJournalPrefab;
	public GameObject PlacesJournalPrefab;
	public GameObject ThingsJournalPrefab;


	public NotificationManager NM;

	public List<int> PeopleSeen;
	public List<int> PlacesSeen;
	public List<int> ThingsSeen;

	void Start()
	{
		PeopleSeen = new List<int>();
		PlacesSeen = new List<int>();
		ThingsSeen = new List<int>();
	}
	public void AddPerson(Person me, bool newInfo)
	{
		if (!PeopleSeen.Contains(me.id))
		{
			PeopleSeen.Add(me.id);
			GameObject p = Instantiate(PeopleJournalPrefab, PeoplePanelContent.transform);
			p.GetComponent<PeopleJournalPrefabScript>().SetUp(me);
			p.transform.name = "person:" + me.id;
			// show the blinking light on the journal prefab
			p.GetComponent<NotificationAnimationScript>().NewInformation();
			// show the blinking lights on the respective overhead tabs
			NM.ShowPeopleNotification();
			NM.ShowNewClueNotification();
		}
		else
		{
			if (newInfo)
			{
				GameObject p = GameObject.Find("person:" + me.id);
				p.GetComponent<NotificationAnimationScript>().NewInformation();
				// show the blinking lights on the respective overhead tabs
				NM.ShowPeopleNotification();
				NM.ShowNewClueNotification();
				Debug.Log("hmm");
			}
		}
	}

	public void AddPlace(City me, bool newInfo)
	{
		Debug.Log(PlacesSeen.Contains(me.id));
		if (!PlacesSeen.Contains(me.id))
		{
			PlacesSeen.Add(me.id);
			GameObject p = Instantiate(PlacesJournalPrefab, PlacesPanelContent.transform);
			p.GetComponent<PlaceJournalPrefabScript>().SetUp(me);
			p.transform.name = "city:" + me.id;
			// show the blinking light on the journal prefab
			p.GetComponent<NotificationAnimationScript>().NewInformation();
			// show the blinking lights on the respective overhead tabs
			NM.ShowPlacesNotification();
			NM.ShowNewClueNotification();
			GM.PlaySoundFX(6);
		}
		else
		{
			if (newInfo)
			{         
				GameObject p = GameObject.Find("city:" + me.id);
				// show the blinking light on the journal prefab
				p.GetComponent<NotificationAnimationScript>().NewInformation();
				// show the blinking lights on the respective overhead tabs
				NM.ShowPlacesNotification();
				NM.ShowNewClueNotification();
				GM.PlaySoundFX(6);
			}
		}
	}

	public void AddItem(Item me, bool newInfo)
	{
		if (!ThingsSeen.Contains(me.id))
		{
			ThingsSeen.Add(me.id);
			GameObject i = Instantiate(ThingsJournalPrefab, ThingsPanelContent.transform);
			i.GetComponent<ItemJournalPrefabScript>().SetUp(me);
			i.transform.name = "item:" + me.id;
			i.GetComponent<NotificationAnimationScript>().NewInformation();
			// show the blinking lights on the respective overhead tabs
			NM.ShowThingsNotification();
			GM.PlaySoundFX(6);
		}
		else if(newInfo)
		{
			GameObject p = GameObject.Find("item:" + me.id);
			// show the blinking light on the journal prefab
			p.GetComponent<NotificationAnimationScript>().NewInformation();
			// show the blinking lights on the respective overhead tabs
			NM.ShowThingsNotification();
			GM.PlaySoundFX(6);
		}
	}


    public void PollPlaces()
    {

    }
}
