using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemJournalPrefabScript : MonoBehaviour {

	public Item Item;
	public Text DescriptionTitle;
	public Text DescriptionContent;

	public DescriptionManager DM;

	// Use this for initialization
	void Start () 
	{
		DM = GameObject.Find("GameManagers").GetComponent<DescriptionManager>();
	}

	public void SetUp(Item me)
	{
		Item = me;
		//change name
		this.GetComponentInChildren<Text>().text = Item.name;
		this.GetComponent<Button>().onClick.AddListener(delegate
		{
			Clicked();
		});
	}

	public void Clicked()
	{
		// set up an item pressed
		DM.ItemPressed(Item);
	}
}
