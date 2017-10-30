﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DescriptionManager : MonoBehaviour {

	/// <summary>
	/// The name.
	/// </summary>
	public Text Name;
	/// <summary>
	/// The description.
	/// </summary>
	public Text Description;

	public Text InhabitantsTitle;

	public Button FoundInCityButton;
	public Button FoundInBuildingButton;
	public Button ArrestButton;

	public GameObject InhabitantsList;

	public GameObject FadePanel;
	public GameObject FadePanelParent;

	// prefab objects
	public GameObject InhabitantPrefab;
	public GameObject TravelHereButton;
	public GameObject FactPrefab;

    public GameManager GM;
	public ConditionManager CM;
	// Use this for initialization
	void Start () {
		Name.text = "This is the Description Panel";
		Description.text = "When you click on any object, place, or person in the game, a description of it will be displayed here.";
		CM = ConditionManager.GetInstance();
	}

	/// <summary>
	/// Sets the description.
	/// </summary>
	/// <param name="title">Title.</param>
	/// <param name="content">Content.</param>
	public void SetDescription(string title, string content)
	{
		Debug.Log("boop");
		FoundInCityButton.gameObject.SetActive(false);
		FoundInBuildingButton.gameObject.SetActive(false);
		TravelHereButton.gameObject.SetActive(false);
		ClearInhabitantsList();
		FadePanelParent.SetActive(false);
		Name.text = title;
		Description.text = content;

		GM.PlaySoundFX(5);
	}


	/// <summary>
	/// Sets the location.
	/// </summary>
	/// <param name="me">Me.</param>
	/// <param name="building">Building.</param>
	/// <param name="city">City.</param>
	public void SetLocation(Person me, Building building, City city)
	{
		int[] conds = me.condition;
		List<int> conditionsList = new List<int>(conds);

		ConditionManager cm = ConditionManager.GetInstance();
		// check to make sure we can find this person
		if (conditionsList.Count == 0 || cm.IsSet(conditionsList))
		{
			FoundInCityButton.gameObject.SetActive(true);
			FoundInBuildingButton.gameObject.SetActive(true);
			FoundInCityButton.GetComponentInChildren<Text>().text = city.name;
			FoundInBuildingButton.GetComponentInChildren<Text>().text = building.name;

			// set up the button
			FoundInCityButton.onClick.RemoveAllListeners();
			FoundInBuildingButton.onClick.RemoveAllListeners();
			FoundInBuildingButton.onClick.AddListener(delegate { BuildingPressed(building); });
			FoundInCityButton.onClick.AddListener(delegate { CityPressed(city); });
		}
		else
		{
			FoundInCityButton.gameObject.SetActive(true);
			FoundInBuildingButton.gameObject.SetActive(true);
			FoundInCityButton.onClick.RemoveAllListeners();
			FoundInBuildingButton.onClick.RemoveAllListeners();

			FoundInCityButton.GetComponentInChildren<Text>().text = "???";
			FoundInBuildingButton.GetComponentInChildren<Text>().text = "???";
		}
	}

	public void SetLocation(Item me, Building building, City city)
	{
		int[] conds = me.condition;
		List<int> conditionsList = new List<int>(conds);

		ConditionManager cm = ConditionManager.GetInstance();

		// check to make sure we can find this item
		if (conditionsList.Count == 0 || cm.IsSet(conditionsList))
		{
			FoundInCityButton.gameObject.SetActive(true);
			FoundInBuildingButton.gameObject.SetActive(true);
			FoundInCityButton.GetComponentInChildren<Text>().text = city.name;
			FoundInBuildingButton.GetComponentInChildren<Text>().text = building.name;

			// set up the button
			FoundInCityButton.onClick.RemoveAllListeners();
			FoundInBuildingButton.onClick.RemoveAllListeners();
			FoundInBuildingButton.onClick.AddListener(delegate { BuildingPressed(building); });
			FoundInCityButton.onClick.AddListener(delegate { CityPressed(city); });
		}
		else
		{
			FoundInCityButton.gameObject.SetActive(true);
			FoundInBuildingButton.gameObject.SetActive(true);
			FoundInCityButton.onClick.RemoveAllListeners();
			FoundInBuildingButton.onClick.RemoveAllListeners();

			FoundInCityButton.GetComponentInChildren<Text>().text = "???";
			FoundInBuildingButton.GetComponentInChildren<Text>().text = "???";
		}
	}

	/// <summary>
	/// Sets the facts.
	/// </summary>
	/// <param name="me">Me.</param>
	public void SetFacts(Person me)
	{
		if (FileReader.TheGameFile.SearchSuspects(me.id) != null)
		{
			InhabitantsTitle.text = "Facts Known";
			// if this is a suspect, show which facts we know
			List<Fact> knownFacts = FileReader.TheGameFile.KnownFacts(me.id);

			foreach (Fact fact in knownFacts)
			{
				GameObject f = Instantiate(FactPrefab, InhabitantsList.transform);
				f.GetComponentInChildren<Text>().text = fact.factoid;
			}
			// set up the arrest button
			ArrestButton.gameObject.SetActive(true);
		}
		else
		{
			FadePanelParent.SetActive(true);
			FadePanel.GetComponent<Image>().overrideSprite = GM.SearchPeopleSprites(me.image);
		}
	}

	public void LoadItemPhoto(Item me)
	{
		FadePanelParent.SetActive(true);
		if (me.itemtype.Equals("photograph"))
		{
			// check if an image exists
			if (me.image.Equals("null"))
			{
				// if it is null, then this is a torn photo. Use the torn photograph
				FadePanel.GetComponent<Image>().overrideSprite = GM.TornPhotographImage;
			}
			// otherwise search for the photo
			else
			{
				Sprite myFace = GM.SearchItemSprites(me.image);
				FadePanel.GetComponent<Image>().overrideSprite = myFace;
			}
		}
		else if (me.itemtype.Equals("key"))
		{
			FadePanel.GetComponent<Image>().overrideSprite = GM.KeyImage;
			Debug.Log("Key Reached");
		}
		else if (me.itemtype.Equals("book"))
		{
			FadePanel.GetComponent<Image>().overrideSprite = GM.BookImage;
		}
		else if (me.itemtype.Equals("flashlight"))
		{
			FadePanel.GetComponent<Image>().overrideSprite = GM.FlashlightImage;
		}
		else if (me.itemtype.Equals("crowbar"))
		{
			FadePanel.GetComponent<Image>().overrideSprite = GM.CrowbarImage;
		}
		else if (me.itemtype.Equals("letter"))
		{
			FadePanel.GetComponent<Image>().overrideSprite = GM.LetterImage;
		}	
	}
	public void SetArrestButton(Person me)
	{
		if (FileReader.TheGameFile.SearchSuspects(me.id) != null)
		{
			ArrestButton.enabled = true;
			ArrestButton.GetComponent<Button>().onClick.RemoveAllListeners();
			ArrestButton.GetComponent<Button>().onClick.AddListener(
				delegate
				{
					ClickedArrest(me);
				});
		}
		else
		{
			ArrestButton.enabled = false;
		}

	}
	/// <summary>
	/// Buildings the pressed.
	/// </summary>
	/// <param name="me">Me.</param>
	public void BuildingPressed(Building me)
	{
		// Display what city this is in, with buttons
		SetDescription(me.name, me.description);
		// enable the city button & set up	
		FoundInCityButton.gameObject.SetActive(true);
		FoundInCityButton.onClick.RemoveAllListeners();
		FoundInCityButton.onClick.AddListener(delegate { CityPressed(FileReader.TheGameFile.SearchCities(me.cityid)); });

		// List who is here, complete with buttons
		int[] pIDs = me.peopleid;
		List<Person> peopleList = new List<Person>();
		// populate the people list
		foreach (int i in pIDs)
		{
			peopleList.Add(FileReader.TheGameFile.SearchPeople(i));
		}
		// change title
		InhabitantsTitle.text = "People Here";
		foreach (Person p in peopleList)
		{
			GameObject inhab = Instantiate(InhabitantPrefab, InhabitantsList.transform);
			inhab.GetComponent<PersonManager>().me = p;
			inhab.GetComponent<PersonManager>().Name.text = p.name;
		}
		// Dont show the ArrestThisPerson button
		ArrestButton.gameObject.SetActive(false);
	}

	/// <summary>
	/// Cities the pressed.
	/// </summary>
	/// <param name="me">Me.</param>
	public void CityPressed(City me)
	{
		SetDescription(me.name, me.description);

		// activate the "Travel Here!" Button
		TravelHereButton.SetActive(true);
		TravelHereButton.GetComponent<Button>().onClick.RemoveAllListeners();
		TravelHereButton.GetComponent<Button>().onClick.AddListener(delegate {
			GM.TravelHere(me);
		});

		// List who is here, complete with buttons
		int[] bIDs = me.buildingid;
		List<Person> peopleList = new List<Person>();

		foreach (int i in bIDs)
		{
			int[] pIDs = FileReader.TheGameFile.SearchBuildings(i).peopleid;
			// populate the people list
			foreach (int p in pIDs)
			{
				peopleList.Add(FileReader.TheGameFile.SearchPeople(p));
			}
		}
		// change title
		InhabitantsTitle.text = "People Here";
		foreach (Person p in peopleList)
		{
			List<int> conditionsList = new List<int>(p.condition);
			Building b = FileReader.TheGameFile.SearchBuildings(p.buildingid);
			List<int> bConditionsList = new List<int>(b.condition);
			// if the building and the person are both visible, show this person
			if (CM.IsSet(conditionsList) && CM.IsSet(bConditionsList))
			{
				GameObject inhab = Instantiate(InhabitantPrefab, InhabitantsList.transform);
				inhab.GetComponent<PersonManager>().me = p;
				inhab.GetComponent<PersonManager>().Name.text = p.name;
			}
		}
		// Dont show the ArrestThisPerson button
		ArrestButton.gameObject.SetActive(false);

	}

	public void ItemPressed(Item me)
	{
		SetDescription(me.name, me.description);

		Building building = FileReader.TheGameFile.SearchBuildings(me.buildingid);
		City city = FileReader.TheGameFile.SearchCities(building.cityid);


	}

	public void ClearInhabitantsList()
	{
		foreach (Transform child in InhabitantsList.transform)
		{
			Destroy(child.gameObject);
		}
	}

	public void ClickedArrest(Person me)
	{
		// check if the person is the actual culprit
		if (me.id == FileReader.TheGameFile.crime.culprit)
		{
			// TODO Win the game!
			Debug.Log("You win!");
			GM.ShowArrestScreen(me, true);
		}
		else
		{
			// TODO Lose the game...
			Debug.Log("You Lose!");
			GM.ShowArrestScreen(me, false);
		}


	} 
}
