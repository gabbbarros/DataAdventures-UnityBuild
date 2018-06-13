using System;
using UnityEngine;
using UnityEngine.UI;
public class BuildingManager
{

	public Building me;
    public Text Name;

	private DescriptionManager DM;


	void Start()
	{
		DM = GameObject.FindWithTag("UI Manager").GetComponent<DescriptionManager>();	
	}

	public void ClickedBuilding()
	{
		DM.SetDescription(me.name, me.description);

	}
}

