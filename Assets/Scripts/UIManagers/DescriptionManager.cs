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
		Name.text = title;
		Description.text = content;
	}

	public void SetLocation(string building, string city)
	{
		FoundInCityButton.GetComponentInChildren<Text>().text = city;
		FoundInBuildingButton.GetComponentInChildren<Text>().text = building;

		// set up the button


	}
	 
}
