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
	public void SetUp(Person p)
	{
		me = p;
		//Photo.sprite = "somesprite.png";
		Name.text = me.name;

		// set up dialogue button
	}

	public void DescriptionTrigger()
	{
		DM.SetDescription(me.name, me.description);


		DM.SetLocation(me, FileReader.TheGameFile.SearchBuildings(me.buildingid), FileReader.TheGameFile.SearchCities(FileReader.TheGameFile.SearchBuildings(me.buildingid).cityid));
	}
}
