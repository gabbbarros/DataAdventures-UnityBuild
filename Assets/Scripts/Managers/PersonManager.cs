using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonManager : MonoBehaviour {
	public Person me;
	public Image Photo;
	public Text Name;
	public Button TalkButton;

	private DescriptionManager DM;
	void Start()
	{
		DM = GameObject.FindWithTag("UI Manager").GetComponent<DescriptionManager>();
	}
	public void SetUp()
	{
		//Photo.sprite = "somesprite.png";
		me = FileReader.TheGameFile.people[0];
		Name.text = me.name;

	}

	public void DescriptionTrigger()
	{
		DM.SetDescription(me.name, me.description);
		DM.SetLocation(FileReader.TheGameFile.SearchBuildings(me.buildingid).name, FileReader.TheGameFile.SearchCities(FileReader.TheGameFile.SearchBuildings(me.buildingid).cityid).name);
	}
}
