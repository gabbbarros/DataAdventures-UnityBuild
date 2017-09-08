using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour {

	// Inventory item prefab for Inventory content
	public GameObject InventoryItemPrefab;
	// Inventory content box
	public GameObject InventoryContent;


	public List<Item> Inventory;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void AddKey()
	{

	}
	public void AddItem(Item me)
	{
		// add the item to the player's inventory list
		Inventory.Add(me);
		// add the item to the UI inventory if it is a Key or Flashlight
		GameObject createdItem = Instantiate(InventoryItemPrefab, InventoryContent.transform) as GameObject;
		createdItem.GetComponent<Text>().text = me.name;
	}

	public void RemoveItem(Item me)
	{
		// remove item from the player's inventory list
		Inventory.Remove(me);
		// remove the item from the UI inventory
	}
}
