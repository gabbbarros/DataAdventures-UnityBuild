using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour {

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
	public void AddPerson(Person me)
	{
		if (!PeopleSeen.Contains(me.id))
		{
			PeopleSeen.Add(me.id);
			GameObject p = Instantiate(PeopleJournalPrefab, PeoplePanelContent.transform);
			p.GetComponent<PeopleJournalPrefabScript>().SetUp(me);
		}
	}

	public void AddPlace(City me)
	{
		if (!PlacesSeen.Contains(me.id))
		{
			PlacesSeen.Add(me.id);
			GameObject p = Instantiate(PlacesJournalPrefab, PlacesPanelContent.transform);
			p.GetComponent<PlaceJournalPrefabScript>().SetUp(me);
		}
	}

}
