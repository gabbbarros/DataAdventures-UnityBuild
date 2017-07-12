using System;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class GameFile
{
	public Person[] people;
	public Item[] items;
	public Building[] buildings;
	public City[] cities;
	public DialogueNode[] dialoguenodes;
	public Crime crime;
	public int ConditionSize { get; set; }

	public Person SearchPeople(int id)
	{
		foreach (Person p in people)
		{
			if (p.id == id)
				return p;
		}
		return null;
	}

	public City SearchCities(int id)
	{
		foreach (City c in cities)
		{
			if(c.id == id)
				return c;
		}
		return null;
	}

	public Item SearchItems(int id)
	{
		foreach (Item i in items)
		{
			if (i.id == id)
				return i;
		}
		return null;
	}

	public Building SearchBuildings(int id)
	{
		foreach (Building b in buildings)
		{
			if (b.id == id)
				return b;
		}
		return null;
	}

	public int InitConditions()
	{
		int max = -1;
		foreach (Person p in people)
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
		}
		ConditionSize = max;
		return max;
	}
}

