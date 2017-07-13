using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SuspectListManager : MonoBehaviour {


	public GameObject SuspectPrefab;
	public GameObject SuspectListContent;
	public DescriptionManager DM;
	public void AddSuspect(Person me)
	{
		GameObject s = Instantiate(SuspectPrefab, SuspectListContent.transform);
		Text[] TextArray = s.GetComponentsInChildren<Text>();
		TextArray[0].text = me.name;
		Button button = s.GetComponent<Button>();
		s.AddComponent(typeof(Suspect));

		Suspect sus = s.GetComponent<Suspect>();
		sus.me = me;
		button.onClick.AddListener(delegate { SuspectClicked(sus); });
	}
	public void SuspectClicked(Suspect sus)
	{
		DM.SetDescription(sus.me.name, sus.me.description);
		DM.SetLocation(sus.me, FileReader.TheGameFile.SearchBuildings(sus.me.buildingid), FileReader.TheGameFile.SearchCities(FileReader.TheGameFile.SearchBuildings(sus.me.buildingid).cityid));


	}


}
