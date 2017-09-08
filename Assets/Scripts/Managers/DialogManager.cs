using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	/// <summary>
	/// The game manager.
	/// </summary>
	public GameManager GM;

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

		// load the person building location
		Location = FileReader.TheGameFile.SearchBuildings(me.buildingid);
		// display person image
		// TODO : swap out game name
		// search for image
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
		PPhoto.GetComponent<Image>().overrideSprite = myFace;


        // display dialogue line at beginning
        PDialogue.text = me.rootnode.dialogueline;

        // lay down the choices
        foreach(DialogueNode child in me.rootnode.childrenNodes)
        {
			AddChoice(child);
        }
		AddExitChoice();
	}

	public void LoadItem(Item me)
	{
		// Set these conditions
		foreach (int cond in me.condition)
		{
			CM.SetAsTrue(cond);
		}

		// display item name
        IName.text = me.name;
		Sprite myFace = null;
		foreach (Sprite img in GM.ItemsSprites)
		{
			Debug.Log(me.image.Remove(me.image.IndexOf('.')));
			if (img.name.Contains(me.image.Remove(me.image.IndexOf('.')))) 
			{
				myFace = img;
				break;
			}
		}
		PPhoto.GetComponent<Image>().overrideSprite = myFace;

		IName.text = "This is " + me.name + ".";
		IDescription.text = me.description;

		// remove all prior listeners
		Button goback = GoBackButton.GetComponent<Button>();
		goback.onClick.RemoveAllListeners();

		// find this item's building
		Building building = FileReader.TheGameFile.SearchBuildings(me.buildingid);

		if (me.name.Equals("Key") || me.name.Equals("Flashlight"))
		{
			// name goback button "Collect"
			goback.GetComponentInChildren<Text>().text = "Collect";

			// add the Collect listener for the goback button
			goback.onClick.AddListener(delegate 
			{
					
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

	public void ClearOptions()
	{
		foreach (Transform child in Options.transform)
		{
			Destroy(child.gameObject);
		}
	}

	public GameObject AddChoice(DialogueNode me)
	{
        GameObject choice = Instantiate(DialogueButtonPrefab, Options.transform);
        DialogueButtonPrefabScript DBPS = choice.GetComponent<DialogueButtonPrefabScript>();

        DBPS.SetLine(me.option);
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
	public void Clicked(DialogueNode me)
	{
		Debug.Log("Node ID: " + me.id);

		// change dialogue line
		PDialogue.text = me.dialogueline;

		ClearOptions();

		// loop through all children and add to dialogue choices
		foreach (DialogueNode child in me.childrenNodes)
		{
			AddChoice(child);
		}

		// add a goodbye exit option
		AddExitChoice();

		// trigger a condition if one exists
		int eventID = me.eventid;
		if (eventID != -1)
		{
			CM.SetAsTrue(eventID);
		}

	}
	
}
