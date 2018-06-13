using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceJournalPrefabScript : MonoBehaviour
{
    public City City;
    public Text DescriptionTitle;
    public Text DescriptionContent;

	public DescriptionManager DM;
    void Start()
    {
		DM = GameObject.Find("GameManagers").GetComponent<DescriptionManager>();
    }

	public void SetUp(City me)
	{
		City = me;
		// change name
		this.GetComponentInChildren<Text>().text = City.name;
		this.gameObject.GetComponent<Button>().onClick.AddListener(delegate
	   {
		   Clicked();
	   });

	}

	public void Clicked()
	{
		// set up a city pressed
		DM.CityPressed(City);
		DM.GM.ALM.AddFileLog("JOURNAL_QUERY_CITY:{" + City.id + ":" + City.name + "}");
		DM.GM.ALM.JournalCityQueryCount++;
		DM.GM.ALM.JournalQueryCount++;

	}
}
