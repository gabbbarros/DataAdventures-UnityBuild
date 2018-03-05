using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

	public InventoryManager IM;

	public SoundFXManager SFXM;

	public FactSoundtrackManager FSM;

	public InquisitionManager IQM;
	/** WinLose Panel Begin **/
	public GameObject WinLosePanel;
	public Text WinLoseDescription;
	public Text WinLoseResult;
	public Animator FadePanel;
	/** WinLose Panel End **/

    /** City Panel Stuff Begin **/
    public GameObject CityPanel;
    public GameObject CityPanelName;

    public GameObject CityMap;
    public GameObject DotHolder;

    public GameObject BuildingDotPrefab;
	public GameObject BuildingDotPrefabFinished;
	public GameObject BuildingDetailsPanel;

	public GameObject InfoPanel;
    /** City Panel Stuff End **/

    /** Building Panel Stuff Begin **/
    public GameObject BuildingPanel;
    public GameObject BuildingPanelName;

    public GameObject BuildingImage;
    public GameObject PeopleHolder;

    public GameObject PeoplePrefab;
	public GameObject ItemsPrefab;

	public GameObject BackButton;
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

	/** Inquisition Panel Stuff Begin **/
	public GameObject InquisitionPanel;
	/** Inquisition Panel Stuff End **/


	public List<Sprite> PeopleSprites;
	public List<Sprite> BuildingSprites;
	public List<Sprite> CitySprites;
	public List<Sprite> ItemsSprites;


	/** Static Image **/
	public Sprite KeyImage;
	public Sprite PhotographImage;
	public Sprite TornPhotographImage;
	public Sprite CrowbarImage;
	public Sprite BookImage;
	public Sprite FlashlightImage;
	public Sprite LetterImage;
	/** Static Image End **/

    // Use this for initialization
    void Start () {
		StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
		// play some sort of loading animation here
		// TODO: Loading Animation

		if (StaticGameInfo.GameName.Equals(""))
		{
			StaticGameInfo.GameName = "Albert_Einstein";
		}
		FileReaderManager.ReadSaveFile();

		int max = FileReader.TheGameFile.InitConditions();
		FileReaderManager.SetUpSuspects();
		// set up condition manager
		ConditionManager cm = ConditionManager.GetInstance();

		cm.Initialize(FileReader.TheGameFile.ConditionSize);

		// start up the logger
		ALM.InitLogging();
		ALM.AddFileLog("Begin Logging Game");
		ALM.AddFileLog(StaticGameInfo.GameName);
		ALM.AddFileLog("******************");
		ALM.FlushLog();

        // build the list of roots and dialogue trees
        List<DNode> roots = DLB.BuildTrees(FileReader.TheGameFile.dialoguenodes);

        // connect roots and people
        List<Person> peeps = new List<Person>(FileReader.TheGameFile.people);
        DLB.AddDialogueRootToPeople(peeps, roots);
		FileReader.TheGameFile.SetCityConditions();
		FileReader.TheGameFile.SetBuildingConditions();
		// Load all images into their respective lists
		// load people list
		Sprite[] Images = Resources.LoadAll<Sprite>(StaticGameInfo.GameName+"/");
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
			else if (me.name.Contains("PHOTO"))
			{
				ItemsSprites.Add(me);
			}
		}


		// reset the counts on keys, flashlights, and crowbars
		IM.ResetInventory();

		// add all cities with no conditions to the journal
		foreach (City c in FileReader.TheGameFile.cities)
		{
			if (c.condition.Length == 0)
			{
				JM.AddPlace(c, false);
			}
		}
		// we want to go to the city of the first building
		Building firstBuilding = null;
		foreach (Building b in FileReader.TheGameFile.buildings)
		{
			if (b.condition.Length == 0)
			{
				firstBuilding = b;
			}
		}
        //firstBuilding = FileReader.TheGameFile.SearchBuildings(0);
        City firstCity = FileReader.TheGameFile.SearchCities(firstBuilding.cityid);
        TravelHere(firstCity);
		IQM.StopEverything();
       // DM.SetDescription(firstCity.name, firstCity.description);
        DM.CityPressed(firstCity);

		FSM.CheckFactCount();


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
		Sprite myFace = SearchCitySprites(me.image);

		CityMap.GetComponent<Image>().overrideSprite = myFace;

		// if the coordinate list doesn't exists, make one
		if (me.coordinates == null)
		{
			me.coordinates = new List<Pair>();
		}
		// add building dots for all buildings that the player can see
		int count = 0;
        foreach (int bID in me.buildingid)
        {
			Debug.Log("Building: " + bID);
            Building b = FileReader.TheGameFile.SearchBuildings(bID);
            int[] conds = b.condition;
            List<int> conditionsList = new List<int>(conds);

			// used to check if we have discovered all discoverable conditions in this building
			HashSet<int> tempConds = new HashSet<int>();
			if (conditionsList.Count == 0 || cm.IsSet(conditionsList))
			{
				foreach (int pID in b.peopleid)
				{
					Person p = FileReader.TheGameFile.SearchPeople(pID);
					Debug.Log("Person: " + pID);
					int[] conds2 = p.condition;
					List<int> conditionsList2 = new List<int>(conds2);
					if (conditionsList2.Count == 0 || cm.IsSet(conditionsList2))
					{
						// go through all person conditions
						foreach (DNode node in p.allNodes)
						{
							if (node.eventid != -1)
							{
								tempConds.Add(node.eventid);
							}
						}
					}
				}

				foreach (int iID in b.itemsid)
				{
					Item i = FileReader.TheGameFile.SearchItems(iID);
					int[] conds2 = i.condition;
					List<int> conditionsList2 = new List<int>(conds2);
					if (!i.isCollected && (conditionsList2.Count == 0 || cm.IsSet(conditionsList2)))
					{
						if (i.eventid != -1)
						{
							tempConds.Add(i.eventid);
						}
					}
				}
			}

            if (conditionsList.Count == 0 || cm.IsSet(conditionsList))
            {
				// spawn the building on the map
				GameObject dot = null;
				if (b.currentConditionCount == tempConds.Count)
				{
					dot = Instantiate(BuildingDotPrefabFinished, DotHolder.transform);
				}
				else
				{
					dot = Instantiate(BuildingDotPrefab, DotHolder.transform);
				}
				dot.GetComponent<BuildingDotPrefabScript>().SetInfoPanel(InfoPanel);

				if (me.coordinates.Count <= count)
				{
					dot.transform.localPosition = new Vector2(Random.Range(-250f, 250f), Random.Range(-110f, 40f));
					me.coordinates.Add(new Pair(dot.transform.localPosition.x, dot.transform.localPosition.y));
				}
				else
				{	
					dot.transform.localPosition = new Vector2(me.coordinates[count].coordX, me.coordinates[count].coordY);
				}

				// Change dot name
				dot.GetComponentInChildren<BuildingDotPrefabScript>().SetName(b.name);
				// give each dot a travel too listener for buildings
				dot.GetComponent<Button>().onClick.RemoveAllListeners();
				dot.GetComponent<Button>().onClick.AddListener( delegate {
					PressedBuildingDot(b, dot);
					PlaySoundFX("dooropen");
					//TravelHere(b);
				});
            }
			count++;
        }
		ALM.AddLog("Clicked on " + me.name);
		// Add to journal
		JM.AddPlace(me, false);
		IQM.StopEverything();
		PlaySoundFX(2);
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

		// add the listener to the back button
		BackButton.GetComponent<Button>().onClick.RemoveAllListeners();
		BackButton.GetComponent<Button>().onClick.AddListener(delegate {
			TravelHere(FileReader.TheGameFile.SearchCities(me.cityid));
			PlaySoundFX("doorshut");
		});

		// Make the building panel the focus
		BuildingPanel.transform.SetAsLastSibling();
		Debug.Log("Building Panel Active");
		//change name to the building name
		BuildingPanelName.GetComponent<Text>().text = me.name;

		// change building image
		Sprite myFace = SearchBuildingSprites(me.image);

		BuildingImage.GetComponent<Image>().overrideSprite = myFace;

		foreach (int pID in me.peopleid)
		{
			Person p = FileReader.TheGameFile.SearchPeople(pID);
//			Debug.Log(pID);
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
				if (!i.name.Equals("Flashlight") && !i.name.Equals("Key") && !i.name.Equals("Crowbar"))
				{
					JM.AddItem(i, false);
				}
			}
		}
		IQM.StopEverything();
		ALM.AddLog("Traveled to " + me.name);
    }



	public void TalkTo(Person me)
	{
		// clear any dialogue
		ClearDialogueHolder();

		if (FileReader.TheGameFile.SearchSuspects(me.id) != null)
		{
			// Make the inquisition panel the focus
			InquisitionPanel.transform.SetAsLastSibling();

			// give control to the inquisition manager
			IQM.Initialize(FileReader.TheGameFile.SearchSuspects(me.id).me);

			DM.PersonPressed(me);

		}
		else
		{
			// Make the dialogue panel the focus
			DialoguePanel.transform.SetAsLastSibling();

			// give control to dialogue manager
			DLM.LoadPerson(me);

			// log it
			ALM.AddLog("Talked to " + me.name);
			ALM.AddFileLog("PERSON_TALK-TO:{" + me.id + ":" + me.name + "}");
			ALM.PersonVisitCount++;
		}
		PlaySoundFX(1);
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
		ALM.AddFileLog("ITEM_INTERACTION:{" + me.id + ":" + me.name + "}");
		ALM.ItemInteractionCount++;
		PlaySoundFX(1);
	}

    public void ChangeOverallFocus(int focus)
    {
        OverallFocus = focus;
    }

    public void ChangeJournalFocus(int focus)
    {
        JournalTabFocus = focus;
    }

	public void PressedBuildingDot(Building me, GameObject dot)
	{

		if (me.locktype.Equals("null") || me.lockBroken)
		{
			TravelHere(me);
			ALM.AddFileLog("BUILDING_TRAVEL:{" + me.id + ":" + me.name + "}");
			ALM.BuildingVisitCount++;
		}
		else
		{
			// activate panel
		BuildingDetailsPanel.SetActive(true);

		// get texts and buttons
		Text[] texts = BuildingDetailsPanel.GetComponentsInChildren<Text>();
		Button[] buttons = BuildingDetailsPanel.GetComponentsInChildren<Button>();

		// change title to the building name
		texts[0].text = me.name;
		if (me.locktype.Equals("dark") && !me.lockBroken)
			{

				// change text to the flashlight question
				texts[1].text = "It is too dark to see within. Do you wish to use a flashlight?";
				// remove all past listeners
				buttons[0].onClick.RemoveAllListeners();
				texts[2].text = "Use";
				// add a Inventory manager event
				buttons[0].onClick.AddListener(delegate
				{
					if (IM.RemoveFlashlight())
					{
						me.lockBroken = true;
						PressedBuildingDot(me, dot);
						BuildingDetailsPanel.SetActive(false);
						ALM.AddFileLog("BUILDING_LIGHT:{" + me.id + ":" + me.name + "}");
					}
				});
			}
			else if (me.locktype.Equals("chain"))
			{
				// change text to the flashlight question
				texts[1].text = "A chain is wound tightly around the entrance. Do you wish to use a crowbar?";
				// remove all past listeners
				buttons[0].onClick.RemoveAllListeners();
				texts[2].text = "Use";
				// add a Inventory manager event
				buttons[0].onClick.AddListener(delegate
				{
					if (IM.RemoveCrowbar())
					{
						me.lockBroken = true;
						PressedBuildingDot(me, dot);
						BuildingDetailsPanel.SetActive(false);
						ALM.AddFileLog("BUILDING_UNCHAIN:{" + me.id + ":" + me.name + "}");
					}
				});
			}
			else if (me.locktype.Equals("key"))
			{
				// change text to the flashlight question
				texts[1].text = "A heavy padlock holds the door shut. Do you wish to use a key?";
				// remove all past listeners
				buttons[0].onClick.RemoveAllListeners();
				texts[2].text = "Use";
				// add a Inventory manager event
				buttons[0].onClick.AddListener(delegate
				{
					if (IM.RemoveKey())
					{
						me.lockBroken = true;
						PressedBuildingDot(me, dot);
						BuildingDetailsPanel.SetActive(false);
						ALM.AddFileLog("BUILDING_UNLOCKED:{" + me.id + ":" + me.name + "}");
					}
				});
			}
		}
	}

	public void PressedBuildingPanelResume()
	{
		BuildingDetailsPanel.SetActive(false);
	}

	public Sprite SearchCitySprites(string name)
	{
		// change building image
		Sprite myFace = null;
		foreach (Sprite img in CitySprites)
		{
			if (img.name.Contains(name.Remove(name.IndexOf('.')))) 
			{
				myFace = img;
				break;
			}
		}
		return myFace;
	}

	public Sprite SearchBuildingSprites(string name)
	{
		// change building image
		Sprite myFace = null;
		foreach (Sprite img in BuildingSprites)
		{
			if (img.name.Contains(name.Remove(name.IndexOf('.'))))
			{
				myFace = img;
				break;
			}
		}
		return myFace;	
	}

	public Sprite SearchPeopleSprites(string name)
	{
		// change building image
		Sprite myFace = null;
		foreach (Sprite img in PeopleSprites)
		{
			if (img.name.Contains(name.Remove(name.IndexOf('.'))))
			{
				myFace = img;
				break;
			}
		}
		return myFace;	
	}

	public Sprite SearchItemSprites(string name)
	{
		// change building image
		Sprite myFace = null;
		Debug.Log("Name : " + name);
		foreach (Sprite img in ItemsSprites)
		{
			Debug.Log("Image searched : " + img.name);
			if (img.name.Contains(name.Remove(name.IndexOf('.'))))
			{
				myFace = img;
				break;
			}
		}
		return myFace;	
	}

	public void PlaySoundFX(int id)
	{
		SFXM.PlaySingle(id);
	}

	public void PlaySoundFX(string type)
	{
		SFXM.PlaySingle(type);
	}

	public void ShowArrestScreen(Person arrestedPerson, bool isWin)
	{
		WinLosePanel.SetActive(true);
		ALM.AddFileLog("SUSPECT_ARREST:{" + arrestedPerson.id + ":" + arrestedPerson.name + "}");
		//TODO show animation or something
		if (isWin)
		{
			WinLoseResult.text = "Mission Success!";
			WinLoseDescription.text = "You arrested " + arrestedPerson.name + " which was the correct call. As the timeline returns "
				+ "to normal, the world around you begins to fade. You always hated this part...";
			ALM.AddFileLog("PLAYER_WINS");
			
		}
		else
		{
			WinLoseResult.text = "Mission Failure...";
			WinLoseDescription.text = "You arrested " + arrestedPerson.name + ", which was the wrong call. The catastrophic ripples "
				+ "through time shake the universal foundations of your world. As the timeline falls apart, "
				+ "you wonder who the real culprit was, and what will happen to you now. The world around you begins to fade...";
			ALM.AddFileLog("PLAYER_LOSES");
		}
		ALM.AddFileLog("**********************");
		ALM.AddFileLog("Quantitative Info Begins:");
		ALM.AddFileLog("CITY VISIT COUNT, BUILDING VISIT COUNT, ITEM INTERACTION COUNT, PERSON VISIT COUNT, DIALOGUE COUNT, JOURNAL QUERY COUNT, JOURNAL ITEM QUERY COUNT, JOURNAL PERSON QUERY COUNT, JOURNAL CITY QUERY COUNT");
		ALM.AddFileLog(ALM.CityTravelCount + "," + ALM.BuildingVisitCount + "," + ALM.ItemInteractionCount + "," + ALM.PersonVisitCount + "," + ALM.DialogueCount + "," + ALM.JournalQueryCount + "," + ALM.JournalItemQueryCount + "," + ALM.JournalPersonQueryCount + "," + ALM.JournalCityQueryCount);
		ALM.FlushLog();
	}

	public void PlayFadeout()
	{
		FadePanel.gameObject.SetActive(true);
		FadePanel.Play("FadeOut2");
	}

	public void ReturnToIntroScene()
	{
		SceneManager.LoadScene("Intro");
	}
}
