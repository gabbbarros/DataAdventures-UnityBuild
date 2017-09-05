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
	public Person SearchSuspects(int id)
	{
		foreach (int s in crime.suspects)
		{
			if (s == id)
			{
				return SearchPeople(s);
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
		Person sus = SearchSuspects(id);

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
		foreach (Fact f in crime.facts)
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
		foreach (Fact f in crime.facts)
		{
			if (f.condition > max)
			{
				max = f.condition;
			}
		}
		ConditionSize = max;
		return max;
	}
}

