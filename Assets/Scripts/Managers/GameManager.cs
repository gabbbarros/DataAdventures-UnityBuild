using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public FileReader FileReaderManager;

    public DescriptionManager DM;


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
        Debug.Log("BEEP");
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

                // give each dot a travel two listener for buildings
            }
        }
    }

    void ClearDotHolder()
    {
        foreach (Transform child in DotHolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void TravelHere(Building me)
    {


    }
}
