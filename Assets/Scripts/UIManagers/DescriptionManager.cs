using System.Collections;
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

	public Button FoundInCityButton;
	public Button FoundInBuildingButton;
	// Use this for initialization
	void Start () {
		Name.text = "This is the Description Panel";
		Description.text = "When you click on any object, place, or person in the game, a description of it will be displayed here.";
		Debug.Log("Boop");
	}

	/// <summary>
	/// Sets the description.
	/// </summary>
	/// <param name="title">Title.</param>
	/// <param name="content">Content.</param>
	public void SetDescription(string title, string content)
	{
		FoundInCityButton.gameObject.SetActive(false);
		FoundInBuildingButton.gameObject.SetActive(false);

		Name.text = title;
		Description.text = content;
	}

	public void SetLocation(Building building, City city)
	{
		FoundInCityButton.gameObject.SetActive(true);
		FoundInBuildingButton.gameObject.SetActive(true);
		FoundInCityButton.GetComponentInChildren<Text>().text = city.name;
		FoundInBuildingButton.GetComponentInChildren<Text>().text = building.name;

		// set up the button
		FoundInCityButton.onClick.RemoveAllListeners();
		FoundInBuildingButton.onClick.AddListener(delegate { BuildingPressed(building); });
		FoundInCityButton.onClick.AddListener(delegate { CityPressed(city); });		
	}

	public void BuildingPressed(Building me)
	{
		// Display what city this is in, with buttons
		SetDescription(me.name, me.description);
		// enable the city button & set up	
		FoundInCityButton.gameObject.SetActive(true);
		FoundInCityButton.onClick.RemoveAllListeners();
		FoundInCityButton.onClick.AddListener(delegate { CityPressed(FileReader.TheGameFile.SearchCities(me.cityid)); });
		// List who is here, complete with buttons


	}

	public void CityPressed(City me)
	{
		SetDescription(me.name, me.description);
		// List who is here, complete with buttons



	}
	 
}
