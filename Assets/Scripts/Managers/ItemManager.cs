using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

	public Item me;
	public GameObject Photo;
	public Text Name;
	public Button TalkButton;

	private DescriptionManager DM;
	private GameManager GM;

	public bool Used { get; set;}
	// Use this for initialization
	void Start () 
	{
		DM = GameObject.FindWithTag("UI Manager").GetComponent<DescriptionManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetUp(Item i)
	{
		GM = GameObject.FindWithTag("UI Manager").GetComponent<GameManager>();
		me = i;

		Name.text = me.name;

		// set up photo
		// search by image
		Sprite myFace = null;

		foreach (Sprite img in GM.ItemsSprites)
		{
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
