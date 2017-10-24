using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

	public Item me;
	public GameObject Photo;
	public Text Name;
	public Button TalkButton;

	public DescriptionManager DM;
	public GameManager GM;

	public bool Used { get; set;}
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetUp(Item i)
	{		
		DM = GameObject.FindWithTag("UI Manager").GetComponent<DescriptionManager>();
		GM = GameObject.FindWithTag("UI Manager").GetComponent<GameManager>();

		me = i;

		Name.text = me.name;
        LoadItemPhoto(me);

		// set up photo
		// search by image
		//TODO: this will be static images
		TalkButton.GetComponentInChildren<Text>().text = "Interact";


	}

	public void DescriptionTrigger()
	{
		DM.SetDescription(me.name, me.description);
		DM.LoadItemPhoto(me);
		DM.SetLocation(me, FileReader.TheGameFile.SearchBuildings(me.buildingid), FileReader.TheGameFile.SearchCities(FileReader.TheGameFile.SearchBuildings(me.buildingid).cityid));
	}
	public void LoadItemPhoto(Item me)
	{
		if (me.itemtype.Equals("photograph"))
		{
			Photo.GetComponent<Image>().overrideSprite = GM.TornPhotographImage;
		}
		else if (me.itemtype.Equals("key"))
		{
			Photo.GetComponent<Image>().overrideSprite = GM.KeyImage;
		}
		else if (me.itemtype.Equals("book"))
		{
			Photo.GetComponent<Image>().overrideSprite = GM.BookImage;
		}
		else if (me.itemtype.Equals("flashlight"))
		{
			Photo.GetComponent<Image>().overrideSprite = GM.FlashlightImage;
		}
		else if (me.itemtype.Equals("crowbar"))
		{
			Photo.GetComponent<Image>().overrideSprite = GM.CrowbarImage;
		}
		else if (me.itemtype.Equals("letter"))
		{
			Photo.GetComponent<Image>().overrideSprite = GM.LetterImage;
		}
	}


}
