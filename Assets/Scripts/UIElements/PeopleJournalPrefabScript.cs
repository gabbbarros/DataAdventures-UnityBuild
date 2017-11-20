using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleJournalPrefabScript : MonoBehaviour
{
    public Person Person;
    public Text DescriptionTitle;
    public Text DescriptionContent;

	public DescriptionManager DM;

	void Start()
	{
		DM = GameObject.Find("GameManagers").GetComponent<DescriptionManager>();
	}

	public void SetUp(Person me)
	{
		Person = me;
		// change names
		this.GetComponentInChildren<Text>().text = Person.name;
		this.gameObject.GetComponent<Button>().onClick.AddListener(delegate
	   {
		   Clicked();
	   });

	}

	public void Clicked()
	{
		// set description
		DM.SetDescription(Person.name, Person.description);
		Building building = FileReader.TheGameFile.SearchBuildings(Person.buildingid);
		City city = FileReader.TheGameFile.SearchCities(building.cityid);
		// set the location
		DM.SetLocation(Person, building, city);

		// if this is a suspect, show which facts we know
		DM.SetFacts(Person);
		//DM.SetArrestButton(Person);

	}
}
