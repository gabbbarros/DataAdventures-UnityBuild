using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonManager : MonoBehaviour {
	public Person me;
	public GameObject Photo;
	public Text Name;
	public Button TalkButton;

	private DescriptionManager DM;
	private GameManager GM;
	void Start()
	{
		DM = GameObject.FindWithTag("UI Manager").GetComponent<DescriptionManager>();
	}
	public void SetUp(Person p)
	{
		GM = GameObject.FindWithTag("UI Manager").GetComponent<GameManager>();
		me = p;

		Name.text = me.name;

		// set up photo
		// seach for image
		Sprite myFace = null;

		foreach (Sprite img in GM.PeopleSprites)
		{
			Debug.Log(me.image.Remove(me.image.IndexOf('.')));
			if (img.name.Contains(me.image.Remove(me.image.IndexOf('.')))) 
			{
				myFace = img;
				break;
			}
		}
		Photo.GetComponentInChildren<Image>().overrideSprite = myFace;
	}

	public void DescriptionTrigger()
	{
		DM.SetDescription(me.name, me.description);


		DM.SetLocation(me, FileReader.TheGameFile.SearchBuildings(me.buildingid), FileReader.TheGameFile.SearchCities(FileReader.TheGameFile.SearchBuildings(me.buildingid).cityid));
	}
}
