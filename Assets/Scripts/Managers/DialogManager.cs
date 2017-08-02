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
	public Text Name;
	/// <summary>
	/// The dialogue the person said.
	/// </summary>
	public Text Dialogue;

    /// <summary>
    /// The image of the person.
    /// </summary>
    public Image Photo;

	public Building Location;

    public GameObject DialogueButtonPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadPerson(Person me)
	{
        // display person name
        Name.text = me.name;

		// load the person building location
		Location = FileReader.TheGameFile.SearchBuildings(me.buildingid);
        // display person image
        // TODO : (do it here)

        // display dialogue line at beginning
        Debug.Log(me.rootnode.id + ": " + me.rootnode.dialogueline);
        Dialogue.text = me.rootnode.dialogueline;

        // lay down the choices
        foreach(DialogueNode child in me.rootnode.childrenNodes)
        {
			//         GameObject choice = Instantiate(DialogueButtonPrefab, Options.transform);
			//         DialogueButtonPrefabScript DBPS = choice.GetComponent<DialogueButtonPrefabScript>();

			//         DBPS.SetLine(child.option);
			//DBPS.me = child;
			//         choice.GetComponent<Button>().onClick.AddListener(
			//             delegate 
			//             {
			//		Clicked(DBPS.me);
			//             });
			AddChoice(child);
        }
		AddExitChoice();
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
		Dialogue.text = me.dialogueline;

		ClearOptions();

		// loop through all children and add to dialogue choices
		foreach (DialogueNode child in me.childrenNodes)
		{
			AddChoice(child);
		}

		// add a goodbye exit option
		AddExitChoice();

	}
	
}
