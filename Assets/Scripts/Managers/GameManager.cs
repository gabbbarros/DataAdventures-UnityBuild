﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {


    /** Keeps track of the current journal tab focus **/

    // 1 = Overview, 2 = Journal, 3 = Activity Manager
    public int OverallFocus;
    // 1 = People, 2 = Places, 3 = Things
    public int JournalTabFocus;

	public FileReader FileReaderManager;

    public DescriptionManager DM;

	public ActivityLogManager ALM;

	public JournalManager JM;

    public DialogManager DLM;

    public DialogueBuilder DLB;

    /** City Panel Stuff Begin **/
    public GameObject CityPanel;
    public GameObject CityPanelName;

    public GameObject CityMap;
    public GameObject DotHolder;

    public GameObject BuildingDotPrefab;
    /** City Panel Stuff End **/

    /** Building Panel Stuff Begin **/
    public GameObject BuildingPanel;
    public GameObject BuildingPanelName;

    public GameObject BuildingImage;
    public GameObject PeopleHolder;

    public GameObject PeoplePrefab;
	public GameObject ItemsPrefab;
	/** Building Panel Stuff End **/


	/** Dialogue Panel Stuff Begin **/
	public GameObject DialoguePanel;
	public GameObject DialoguePanelName;
	public GameObject InteractPanel;
	public GameObject InteractPanelName;

	public GameObject PersonImage;
	public GameObject DialogueHolder;

	public GameObject DialoguePrefab;
	/** Dialogue Panel Stuff End **/

	public List<Sprite> PeopleSprites;
	public List<Sprite> BuildingSprites;
	public List<Sprite> CitySprites;
	public List<Sprite> ItemsSprites;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
        // play some sort of loading animation here
		// TODO: Loading Animation


		FileReaderManager.ReadSaveFile();

		int max = FileReader.TheGameFile.InitConditions();
		FileReaderManager.SetUpSuspects();
		// set up condition manager
		ConditionManager cm = ConditionManager.GetInstance();

		cm.Initialize(FileReader.TheGameFile.ConditionSize);

        // build the list of roots and dialogue trees
        List<DialogueNode> roots = DLB.BuildTrees(FileReader.TheGameFile.dialoguenodes);

        // connect roots and people
        List<Person> peeps = new List<Person>(FileReader.TheGameFile.people);
        DLB.AddDialogueRootToPeople(peeps, roots);

		// Load all images into their respective lists
		// load people list
		Sprite[] Images = Resources.LoadAll<Sprite>("Albert_Einstein/");
		foreach (Sprite me in Images)
		{
			if (me.name.Contains("PERSON"))
			{
				PeopleSprites.Add(me);
			}
			else if (me.name.Contains("BUILDING"))
			{
				BuildingSprites.Add(me);
			}
			else if (me.name.Contains("CITY"))
			{
				CitySprites.Add(me);
			}
			else if (me.name.Contains("ITEM"))
			{
				ItemsSprites.Add(me);
			}
		}


		// reset the counts on keys, flashlights, and crowbars


        // we want to go to the city of the first building
        Building firstBuilding = FileReader.TheGameFile.SearchBuildings(0);
        City firstCity = FileReader.TheGameFile.SearchCities(firstBuilding.cityid);
        TravelHere(firstCity);
       // DM.SetDescription(firstCity.name, firstCity.description);
        DM.CityPressed(firstCity);
	}


    public void TravelHere(City me)
    {
        // clear the DotHolder of all dots
        ClearDotHolder();

        // get condition manager for bitset checking
        ConditionManager cm = ConditionManager.GetInstance();
        // Make the city panel the focus
        CityPanel.transform.SetAsLastSibling();
        Debug.Log("City Panel Active");
		// change name to the city name
		CityPanelName.GetComponent<Text>().text = me.name;

		// change city image
		Sprite myFace = null;
		foreach (Sprite img in CitySprites)
		{
			Debug.Log(me.image.Remove(me.image.IndexOf('.')));
			if (img.name.Contains(me.image.Remove(me.image.IndexOf('.')))) 
			{
				myFace = img;
				break;
			}
		}
		CityMap.GetComponent<Image>().overrideSprite = myFace;
        // add building dots for all buildings that the player can see
        foreach (int bID in me.buildingid)
        {
            Building b = FileReader.TheGameFile.SearchBuildings(bID);
            int[] conds = b.condition;
            List<int> conditionsList = new List<int>(conds);

            if (conditionsList.Count == 0 || cm.IsSet(conditionsList))
            {
                // spawn the building on the map
                GameObject dot = Instantiate(BuildingDotPrefab, DotHolder.transform);
				// TODO: randomize this
				dot.transform.localPosition = new Vector2(Random.Range(-250f, 250f), Random.Range(-130f, 80f));


				// Change dot name
				dot.GetComponentInChildren<BuildingDotPrefabScript>().SetName(b.name);
				// give each dot a travel too listener for buildings
				dot.GetComponent<Button>().onClick.RemoveAllListeners();
				dot.GetComponent<Button>().onClick.AddListener( delegate {
					TravelHere(b);
				});
            }
        }
		ALM.AddLog("Clicked on " + me.name);

		// Add to journal
		JM.AddPlace(me, false);
    }

    void ClearDotHolder()
    {
        foreach (Transform child in DotHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

	void ClearPeopleHolder()
	{
		foreach (Transform child in PeopleHolder.transform)
		{
			Destroy(child.gameObject);
		}
	}

	void ClearDialogueHolder()
	{
		foreach (Transform child in DialogueHolder.transform)
		{
			Destroy(child.gameObject);
		}
	}
    public void TravelHere(Building me)
    {

		ClearPeopleHolder();

		ConditionManager cm = ConditionManager.GetInstance();

		// Make the building panel the focus
		BuildingPanel.transform.SetAsLastSibling();
		Debug.Log("Building Panel Active");
		//change name to the building name
		BuildingPanelName.GetComponent<Text>().text = me.name;

		// change building image
		Sprite myFace = null;
		foreach (Sprite img in BuildingSprites)
		{
			Debug.Log(me.image.Remove(me.image.IndexOf('.')));
			if (img.name.Contains(me.image.Remove(me.image.IndexOf('.')))) 
			{
				myFace = img;
				break;
			}
		}
		BuildingImage.GetComponent<Image>().overrideSprite = myFace;

		foreach (int pID in me.peopleid)
		{
			Person p = FileReader.TheGameFile.SearchPeople(pID);
			int[] conds = p.condition;
			List<int> conditionsList = new List<int>(conds);
			if (conditionsList.Count == 0 || cm.IsSet(conditionsList))
			{
				// spawn a person
				GameObject person = Instantiate(PeoplePrefab, PeopleHolder.transform);

				// change person name
				person.GetComponent<PersonManager>().SetUp(p);
				person.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
				person.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate
				{
					TalkTo(p);
				});

				// add people to journal
				JM.AddPerson(p, false);
			}
		}
		foreach (int iID in me.itemsid)
		{
			Item i = FileReader.TheGameFile.SearchItems(iID);
			int[] conds = i.condition;
			List<int> conditionsList = new List<int>(conds);
			if (!i.isCollected && (conditionsList.Count == 0 || cm.IsSet(conditionsList)))
			{
				// spawn an item
				GameObject item = Instantiate(ItemsPrefab, PeopleHolder.transform);
				// change item name
				item.GetComponent<ItemManager>().SetUp(i);
				item.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
				item.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate 
				{
					InteractWith(i);
				});

				if (!i.name.Equals("Flashlight") && !i.name.Equals("Key"))
				{
					JM.AddItem(i, false);
				}
			}
		}
		ALM.AddLog("Traveled to " + me.name);
    }



	public void TalkTo(Person me)
	{
		// clear any dialogue
		ClearDialogueHolder();

		// Make the dialogue panel the focus
		DialoguePanel.transform.SetAsLastSibling();

        // give control to dialogue manager
        DLM.LoadPerson(me);

		// log it
		ALM.AddLog("Talked to " + me.name);
	}

	public void InteractWith(Item me)
	{
		// uh what would this show?
		ClearDialogueHolder();
		// Make the dialogue panel the focus
		InteractPanel.transform.SetAsLastSibling();

		// give control to dialogue manager
		DLM.LoadItem(me);

		// log it
		ALM.AddLog("Interacted with " + me.name);


	}

    public void ChangeOverallFocus(int focus)
    {
        OverallFocus = focus;
    }

    public void ChangeJournalFocus(int focus)
    {
        JournalTabFocus = focus;
    }


}
