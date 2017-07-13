using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelHereScript : MonoBehaviour {

 //   /** City Panel Stuff Begin **/
	//public GameObject CityPanel;
	//public GameObject CityPanelName;

	//public GameObject CityMap;
	//public GameObject DotHolder;

	//public GameObject BuildingDotPrefab;
 //   /** City Panel Stuff End **/

 //   /** Building Panel Stuff Begin **/
 //   public GameObject BuildingPanel;
 //   public GameObject BuildingPanelName;

 //   public GameObject BuildingImage;
 //   public GameObject PeopleHolder;

 //   public GameObject PeoplePrefab;
 //   /** Building Panel Stuff End **/

	//public void TravelHere(City me)
	//{
	//	// clear the DotHolder of all dots
	//	ClearDotHolder();

	//	// get condition manager for bitset checking
	//	ConditionManager cm = ConditionManager.GetInstance();
	//	// Make the city panel the focus
	//	CityPanel.transform.SetAsLastSibling();

	//	// add building dots for all buildings that the player can see
	//	foreach (int bID in me.buildingid)
	//	{
	//		Building b = FileReader.TheGameFile.SearchBuildings(bID);
	//		int[] conds = b.condition;
	//		List<int> conditionsList = new List<int>(conds);

 //           if (conditionsList.Count == 0 || cm.IsSet(conditionsList))
	//		{
	//			// spawn the building on the map
	//			GameObject dot = Instantiate(BuildingDotPrefab, DotHolder.transform);
	//			dot.transform.localPosition = new Vector2(100f, 80f);

 //               // give each dot a travel two listener for buildings
	//		}
	//	}
	//}

	//void ClearDotHolder()
	//{
	//	foreach (Transform child in DotHolder.transform)
	//	{
	//		Destroy(child);
	//	}
	//}

 //   public void TravelHere(Building me)
 //   {


 //   }
}
