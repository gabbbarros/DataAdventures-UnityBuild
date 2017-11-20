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
		//s.AddComponent(typeof(Suspect));

		//Suspect sus = s.GetComponent<Suspect>();
		//sus.me = me;
		button.onClick.AddListener(delegate { SuspectClicked(me); });
	}
	public void SuspectClicked(Person me)
	{
		// suspects dont get a description
		DM.SetDescription(me.name, "");
		DM.SetLocation(me, FileReader.TheGameFile.SearchBuildings(me.buildingid), FileReader.TheGameFile.SearchCities(FileReader.TheGameFile.SearchBuildings(me.buildingid).cityid));
		// if this is a suspect, show which facts we know
		DM.SetFacts(me);
		//DM.SetArrestButton(sus.me);
	}


}
