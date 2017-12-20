﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameFile
{
	public Person[] people;
	public Item[] items;
	public Building[] buildings;
	public City[] cities;
	public DialogueNode[] dialoguenodes;
	public Crime crime;
	public Fact[] facts;
	public int ConditionSize { get; set; }


	public List<Item> Keys;
	public List<Item> Lights;
	public List<Item> Crowbars;
	public List<Building> Locks;
	public List<Building> Darks;
	public List<Building> Chains;
	public List<Suspect> Suspects;
	/// <summary>
	/// Searchs the people.
	/// </summary>
	/// <returns>The people.</returns>
	/// <param name="id">Identifier.</param>
	public Person SearchPeople(int id)
	{
		foreach (Person p in people)
		{
			if (p.id == id)
				return p;
		}
		return null;
	}



	/// <summary>
	/// Searchs the cities.
	/// </summary>
	/// <returns>The cities.</returns>
	/// <param name="id">Identifier.</param>
	public City SearchCities(int id)
	{
		foreach (City c in cities)
		{
			if(c.id == id)
				return c;
		}
		return null;
	}
	/// <summary>
	/// Searchs the items.
	/// </summary>
	/// <returns>The items.</returns>
	/// <param name="id">Identifier.</param>
	public Item SearchItems(int id)
	{
		foreach (Item i in items)
		{
			if (i.id == id)
				return i;
		}
		return null;
	}
	/// <summary>
	/// Searchs the buildings.
	/// </summary>
	/// <returns>The buildings.</returns>
	/// <param name="id">Identifier.</param>
	public Building SearchBuildings(int id)
	{
		foreach (Building b in buildings)
		{
			if (b.id == id)
				return b;
		}
		return null;
	}

	/// <summary>
	/// Searchs the suspects.
	/// </summary>
	/// <returns>The suspects.</returns>
	/// <param name="id">Identifier.</param>
	public Suspect SearchSuspects(int id)
	{
		for (int i = 0; i < Suspects.Count; i++)
		{
			//Debug.Log(id + " vs " + Suspects[i].me.id + " = " + (Suspects[i].me.id == id));
			//Debug.Log(Suspects[i].me.name);
			//Debug.Log(Suspects[i].me);
			//Debug.Log(Suspects[i]);
			if (Suspects[i].me.id == id)
			{
				
				return Suspects[i];
			}
		}
		return null;
	}


	/// <summary>
	/// Knowns the facts.
	/// </summary>
	/// <returns>The facts.</returns>
	/// <param name="id">Identifier.</param>
	public List<Fact> KnownFacts(int id)
	{
		ConditionManager cm = ConditionManager.GetInstance();
		Person sus = SearchSuspects(id).me;

		List<Fact> knownFacts = new List<Fact>();
		if (sus != null)
		{
			List<Fact> allFacts = SearchFacts(id);
			foreach (Fact f in allFacts)
			{
				if (cm.IsSet(f.condition))
				{
					knownFacts.Add(f);
				}
			}
		}
		return knownFacts;
	}

	/// <summary>
	/// Searchs the facts.
	/// </summary>
	/// <returns>The facts.</returns>
	/// <param name="id">Identifier.</param>
	public List<Fact> SearchFacts(int id)
	{
		List<Fact> returnMe = new List<Fact>();
		foreach (Fact f in facts)
		{
			if (f.pid == id)
			{
				returnMe.Add(f);
			}
		}
		return returnMe;
	}

	public int InitConditions()
	{
		int max = -1;
		/*foreach (Person p in people)
		{
			foreach (int cond in p.condition)
			{
				if (cond > max)
				{
					max = cond;
				}
			}
		}
		foreach (Building b in buildings)
		{
			foreach (int cond in b.condition)
			{

				if (cond > max)
				{
					max = cond;
				}
			}
		}
		foreach (City c in cities)
		{
			foreach (int cond in c.condition)
			{

				if (cond > max)
				{
					max = cond;
				}
			}
		}
		foreach (Item i in items)
		{
			foreach (int cond in i.condition)
			{

				if (cond > max)
				{
					max = cond;
				}
			}
		}*/
		foreach (Fact f in facts)
		{
			if (f.condition > max)
			{
				max = f.condition;
			}
		}
		ConditionSize = max;
		return max;
	}

	public int GetSuspectCount()
	{
		//int counter = 0;
		//foreach (Person p in people)
		//{
		//	if (FileReader.TheGameFile.SearchSuspects(p.id) != null)
		//	{
		//		counter++;
		//	}
			   
		//}

		return Suspects.Count;
	}

	public void SetUpSuspects()
	{
		List<Fact> returnMe = new List<Fact>();
		Suspects = new List<Suspect>();
		int counter = 0;
		foreach (Fact f in facts)
		{
			if (f.pid == crime.culprit)
			{
				counter++;
			}
		}

		foreach (int s in crime.suspects)
		{
			Debug.Log("Added: " + SearchPeople(s).name);
			Suspect t = new Suspect(counter, SearchPeople(s));
			Debug.Log("Thus Added: " + t);
			Suspects.Add(t);
		}
	}

	public void SetCityConditions()
	{
		foreach (City city in cities)
		{
			Debug.Log(city.name);
			city.eventConditions = new HashSet<int>();
			city.discoveredEventConditions = new HashSet<int>();
			foreach (int buildingid in city.buildingid)
			{
				Building building = SearchBuildings(buildingid);
				foreach (int personid in building.peopleid)
				{
					Person person = SearchPeople(personid);
					foreach(DNode node in person.allNodes)
					{
						if (node.eventid != -1)
						{
							city.eventConditions.Add(node.eventid);
							Debug.Log(node.eventid);
						}
						//foreach (int condition in node.condition)
						//{
						//	if (condition != -1)
						//	{
						//		city.eventConditions.Add(condition);
						//		Debug.Log(condition);
						//	}
						//}
					}
				}
				foreach (int itemid in building.itemsid)
				{
					
					Item item = SearchItems(itemid);
					Debug.Log(item.name + " : " + item.eventid);
					if (item.eventid != -1)
					{
						city.eventConditions.Add(item.eventid);
						Debug.Log(item.eventid);
					}
				}
			}
			city.totalConditionCount = city.eventConditions.Count;
			city.currentConditionCount = 0;
		}
	}
}

