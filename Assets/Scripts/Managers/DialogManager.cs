using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	/// <summary>
	/// The game manager.
	/// </summary>
	public GameManager GM;
	public InventoryManager IM;

	/// <summary>
	/// The options.
	/// </summary>
	public GameObject Options;

	/// <summary>
	/// The name.
	/// </summary>
	public Text PName;
	public Text IName;
	/// <summary>
	/// The dialogue the person said.
	/// </summary>
	public Text PDialogue;
	public Text IDescription;
    /// <summary>
    /// The image of the person.
    /// </summary>
	public GameObject PPhoto;

	/// <summary>
	/// The image of the item
	/// </summary>
	public GameObject IPhoto;
	public GameObject GoBackButton;

	public Building Location;

    public GameObject DialogueButtonPrefab;



	private Person currentPerson;
	private ConditionManager CM;
	// Use this for initialization
	void Start () {
		CM = ConditionManager.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadPerson(Person me)
	{
        // display person name
        PName.text = me.name;
		currentPerson = me;
		// load the person building location
		Location = FileReader.TheGameFile.SearchBuildings(me.buildingid);
		// display person image
		// TODO : swap out game name
		// search for image
		Sprite myFace = GM.SearchPeopleSprites(me.image);

		PPhoto.GetComponent<Image>().overrideSprite = myFace;


        // display dialogue line at beginning
        PDialogue.text = me.rootnode.dialogueline;

        // lay down the choices
        foreach(DNode child in me.rootnode.childrenNodes)
        {
			AddChoice(child);
        }
		AddExitChoice();
	}

	public void LoadItem(Item me)
	{
		// set the event condition
		CM.SetAsTrue(me.eventid);

		// set the event for the city this item is in
		// add this event to the city events
		City city = FileReader.TheGameFile.SearchCities(FileReader.TheGameFile.SearchBuildings(me.buildingid).cityid);
		Debug.Log(me.eventid);

        if (me.eventid != -1)
        {
            city.discoveredEventConditions.Add(me.eventid);
            Debug.Log(city.discoveredEventConditions);
            city.currentConditionCount = city.discoveredEventConditions.Count;
        }
		if (city.Equals(GM.DM.LoadedThing))
		{
			GM.DM.SetFactDiscoveryCounters(city);
		}

		// display item name
        IName.text = me.name;

		LoadItemPhoto(me);

		IName.text = "This is " + me.name + ".";
		IDescription.text = me.description;

		// remove all prior listeners
		Button goback = GoBackButton.GetComponent<Button>();
		goback.onClick.RemoveAllListeners();

		// find this item's building
		Building building = FileReader.TheGameFile.SearchBuildings(me.buildingid);

		if (me.name.Equals("Key") || me.name.Equals("Flashlight") || me.name.Equals("Crowbar"))
		{
			// name goback button "Collect"
			goback.GetComponentInChildren<Text>().text = "Collect";

			// add the Collect listener for the goback button
			goback.onClick.AddListener(delegate 
			{
				Collect(me);
				GM.TravelHere(building);
				if (me.name.Equals("Flashlight"))
				{
					IM.AddFlashlight();
				}
				else if (me.name.Equals("Key"))
				{
					IM.AddKey();
				}
				else if (me.name.Equals("Crowbar"))
				{
					IM.AddCrowbar();
				}
			});
		}
		else
		{
			// name goback button "Go Back"
			goback.GetComponentInChildren<Text>().text = "Go Back";

			// add the go back onlick listener for the goback button
			goback.onClick.AddListener(delegate
			{
				GM.TravelHere(building);
			});

		}
	}


	public void LoadItemPhoto(Item me)
	{
		Debug.Log("Item ID is : " + me.id);
		Debug.Log("Item type is : " + me.itemtype);
		// if item is a photo
		if (me.itemtype.Equals("photograph"))
		{
			// check if an image exists
			if (me.image.Equals("null"))
			{
				// if it is null, then this is a torn photo. Use the torn photograph
				IPhoto.GetComponent<Image>().overrideSprite = GM.TornPhotographImage;
			}
			// otherwise search for the photo
			else
			{
				Sprite myFace = GM.SearchItemSprites(me.image);
				IPhoto.GetComponent<Image>().overrideSprite = myFace;
			}
		}
		else if (me.itemtype.Equals("key"))
		{
			IPhoto.GetComponent<Image>().overrideSprite = GM.KeyImage;
		}
		else if (me.itemtype.Equals("book"))
		{
			IPhoto.GetComponent<Image>().overrideSprite = GM.BookImage;
			Debug.Log("here");
		}
		else if (me.itemtype.Equals("flashlight"))
		{
			IPhoto.GetComponent<Image>().overrideSprite = GM.FlashlightImage;
		}
		else if (me.itemtype.Equals("crowbar"))
		{
			IPhoto.GetComponent<Image>().overrideSprite = GM.CrowbarImage;
		}
		else if (me.itemtype.Equals("letter"))
		{
			IPhoto.GetComponent<Image>().overrideSprite = GM.LetterImage;			
		}

	}
	/// <summary>
	/// Collect this item.
	/// </summary>
	public void Collect(Item me)
	{
		me.isCollected = true;
		GM.PlaySoundFX(0);
	}

	public void ClearOptions()
	{
		foreach (Transform child in Options.transform)
		{
			Destroy(child.gameObject);
		}
	}

	public GameObject AddChoice(DNode me)
	{
        GameObject choice = Instantiate(DialogueButtonPrefab, Options.transform);
        DialogueButtonPrefabScript DBPS = choice.GetComponent<DialogueButtonPrefabScript>();

		if (!me.isVisited || me.dialoguetype != 100)
		{
			DBPS.SetLine(me.option);
		}
		else
		{
			DBPS.SetLine("Thanks. I have a few more questions.");
		}
		DBPS.me = me;

        choice.GetComponent<Button>().onClick.AddListener(
            delegate {
                Clicked(me);
            });
		return choice;
	}

	public GameObject AddExitChoice()
	{
		GameObject choice = Instantiate(DialogueButtonPrefab, Options.transform);
		DialogueButtonPrefabScript DBPS = choice.GetComponent<DialogueButtonPrefabScript>();

		DBPS.SetLine("Goodbye");

		choice.GetComponent<Button>().onClick.AddListener(
			delegate {
				// add exit functionality
				
				GM.TravelHere(Location);
		});

		return choice;
	}
	/// <summary>
	/// Populates the list with all dialogue choice children when pressed
	/// </summary>
	public void Clicked(DNode me)
	{
		if (!me.isVisited)
		{
			me.isVisited = true;
		}

		// change dialogue line
		//PDialogue.text = me.dialogueline;
		SetDialogueLine(me);

		ClearOptions();

		// loop through all children and add to dialogue choices
		foreach (DNode child in me.childrenNodes)
		{
			AddChoice(child);
		}

		// add a goodbye exit option
		AddExitChoice();
		Debug.Log(me.eventid + " : " + me.dialogueline);
		// trigger a condition if one exists
		int eventID = me.eventid;
		if (eventID != -1)
		{
			//GM.PlaySoundFX(7);
			if (!CM.IsSet(eventID)) {
				CM.SetAsTrue(eventID);
				// add this event to the city events
				City city = FileReader.TheGameFile.SearchCities(FileReader.TheGameFile.SearchBuildings(currentPerson.buildingid).cityid);
				Debug.Log(eventID);

				city.discoveredEventConditions.Add(eventID);
				Debug.Log(city.discoveredEventConditions);
				city.currentConditionCount = city.discoveredEventConditions.Count;

				if (city.Equals(GM.DM.LoadedThing))
				{
					GM.DM.SetFactDiscoveryCounters(city);
				}
				// also if this is a suspect fact node, then play a special sound
				if (me.dialoguetype == 616)
				{
					GM.SFXM.PlaySingle(7);
					Person p = GM.FSM.CheckFactCount();

					GM.JM.NM.ShowNewFactNotification(p);
				}

                // check to see if every discoverable condition has been met in this city so far
                HashSet<int> tempConds = new HashSet<int>();
                Debug.Log("Events");

                // check every building in this city, if they are discoverable, then poll for items and people
                foreach (int bID in city.buildingid)
                {
                    Debug.Log("Building: " + bID);
                    Building b = FileReader.TheGameFile.SearchBuildings(bID);
                    int[] conds = b.condition;
                    List<int> conditionsList = new List<int>(conds);
                    if (conditionsList.Count == 0 || CM.IsSet(conditionsList))
                    {
                        foreach (int pID in b.peopleid)
                        {
                            Person p = FileReader.TheGameFile.SearchPeople(pID);
                            Debug.Log("Person: " + pID);
                            int[] conds2 = p.condition;
                            List<int> conditionsList2 = new List<int>(conds2);
                            if (conditionsList2.Count == 0 || CM.IsSet(conditionsList2))
                            {
                                // go through all person conditions
                                foreach (DNode node in p.allNodes)
                                {
                                    if (node.eventid != -1)
                                    {
                                       tempConds.Add(node.eventid);
                                    }
                                    //foreach (int condition in node.condition)
                                    //{
                                    //	if (condition != -1)
                                    //	{
                                    //		city.eventConditions.Add(condition);
                                    //		Debug.Log(condition);
                                    //	}
                                    //}
                                }
                            }
                        }
                        foreach (int iID in b.itemsid)
                        {
                            Item i = FileReader.TheGameFile.SearchItems(iID);
                            int[] conds2 = i.condition;
                            List<int> conditionsList2 = new List<int>(conds2);
                            if (!i.isCollected && (conditionsList2.Count == 0 || CM.IsSet(conditionsList2)))
                            {
                                if (i.eventid != -1)
                                {
                                    tempConds.Add(i.eventid);
                                }
                            }
                        }
                    }
                }
                Debug.Log("TempConds size : " + tempConds.Count);
                Debug.Log("Current Discovered Cond Count : " + city.currentConditionCount);
                if(tempConds.Count == city.currentConditionCount)
                {
                    GameObject p = GameObject.Find("city:" + city.id);
                    // show the blinking light on the journal prefab
                    p.GetComponent<NotificationAnimationScript>().StopInformation();
                }
			}
		}

	}

	public void SetDialogueLine(DNode me)
	{
		string line = me.dialogueline;

		//Debug.Log("Keywords");
		foreach (string keyword in me.keywords)
		{
			//Debug.Log(keyword);
			// if the line contains this keyword
			if (line.Contains(keyword))
			{
				// then find the position, replace this specific part with a rich text version with tags
				int index = line.IndexOf(keyword);
				//Debug.Log(index);
				//Debug.Log(line);
				//line.Remove(index, keyword.Length);
				line = line.Replace(keyword, "<color=lime>" + keyword + "</color>");
				//	line.Insert(index, "<b>" + keyword + "</b>");
				Debug.Log(line);

			}
		}

		PDialogue.text = line;
	}
	
}
