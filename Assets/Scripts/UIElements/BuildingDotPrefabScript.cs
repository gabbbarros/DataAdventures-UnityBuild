using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingDotPrefabScript : MonoBehaviour {

	public GameObject InfoPanel;
	public string name;

	public void SetInfoPanel(GameObject IP)
	{
		InfoPanel = IP;
	}
	public void SetName(string name)
	{
		this.name = name;
	}

	public void HoveringOverEntrance()
	{
		// activate panel
		InfoPanel.SetActive(true);

		// get texts and buttons
		Text[] texts = InfoPanel.GetComponentsInChildren<Text>();

		// change title to the building name
		texts[0].text = name;
	}

	public void HoveringOverExit()
	{
		InfoPanel.SetActive(false);
	}
}
