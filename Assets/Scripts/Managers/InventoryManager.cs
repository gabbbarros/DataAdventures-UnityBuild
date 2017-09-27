using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour {

	// Inventory item prefab for Inventory content
	public GameObject InventoryItemPrefab;
	// Inventory content box
	public GameObject InventoryContent;

	public Text KeyCounter;
	public Text FlashlightCounter;
	public Text CrowbarCounter;

	public int keycount;
	public int flashlightcount;
	public int crowbarcount;

	public List<Item> Inventory;
	// Use this for initialization
	void Start () {
		keycount = 0;
		flashlightcount = 0;
		crowbarcount = 0;

		RefreshKeyUI();
		RefreshFlashlightUI();
		RefreshCrowbarUI();
	}

	// Update is called once per frame
	void Update () 
	{

	}

	/// <summary>
	/// Adds a key.
	/// </summary>
	public void AddKey()
	{
		keycount++;
        RefreshKeyUI();
	}

	/// <summary>
	/// Adds a flashlight.
	/// </summary>
	public void AddFlashlight()
	{
		flashlightcount++;
		RefreshFlashlightUI();
	}
	/// <summary>
	/// Adds a crowbar.
	/// </summary>
	public void AddCrowbar()
	{
		crowbarcount++;
		RefreshCrowbarUI();
	}

	/// <summary>
	/// Removes a key.
	/// </summary>
	public void RemoveKey()
	{
		keycount--;
        RefreshKeyUI();
	}
	/// <summary>
	/// Removes a crowbar.
	/// </summary>
	public void RemoveCrowbar()
	{
		crowbarcount--;
		RefreshCrowbarUI();	
	}
	/// <summary>
	/// Removes a flashlight.
	/// </summary>
	public void RemoveFlashlight()
	{
		flashlightcount--;
		RefreshFlashlightUI();
	}

	/// <summary>
	/// Refreshs the flashlight user interface.
	/// </summary>
	public void RefreshFlashlightUI()
	{
		FlashlightCounter.text = "x " + flashlightcount;
	}
	/// <summary>
	/// Refreshs the crowbar user interface.
	/// </summary>
	public void RefreshCrowbarUI()
	{
		CrowbarCounter.text = "x " + crowbarcount;
	}
	/// <summary>
	/// Refreshs the key user interface.
	/// </summary>
	public void RefreshKeyUI()
	{
		KeyCounter.text = "x " + keycount;
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

	/// <summary>
	/// Resets the inventory.
	/// </summary>
	public void ResetInventory()
	{
		keycount = 0;
		flashlightcount = 0;
		crowbarcount = 0;
		RefreshKeyUI();
		RefreshCrowbarUI();
		RefreshFlashlightUI();
	}
}
