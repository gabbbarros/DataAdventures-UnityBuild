using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {


	public FileReader FileReaderManager;

    public DescriptionManager DM;

	public ActivityLogManager ALM;

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
    /** Building Panel Stuff End **/




    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
		FileReaderManager.ReadSaveFile();
		int max = FileReader.TheGameFile.InitConditions();
		FileReaderManager.SetUpSuspects();
		// set up condition manager
		ConditionManager cm = ConditionManager.GetInstance();

		cm.Initialize(FileReader.TheGameFile.ConditionSize);

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
                dot.transform.localPosition = new Vector2(100f, 80f);

				// Change dot name
				dot.GetComponentInChildren<BuildingDotPrefabScript>().SetName(b.name);
				// give each dot a travel too listener for buildings
				dot.GetComponent<Button>().onClick.RemoveAllListeners();
				dot.GetComponent<Button>().onClick.AddListener( delegate {
					TravelHere(b);
				});
            }
        }
		ALM.AddLog("Traveled to " + me.name);
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

    public void TravelHere(Building me)
    {

		ClearPeopleHolder();

		ConditionManager cm = ConditionManager.GetInstance();

		// Make the building panel the focus
		BuildingPanel.transform.SetAsLastSibling();
		Debug.Log("Building Panel Active");
		//change name to the building name
		BuildingPanelName.GetComponent<Text>().text = me.name;


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
			}
		}
		ALM.AddLog("Traveled to " + me.name);
    }
}
