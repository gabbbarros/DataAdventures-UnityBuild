﻿using System;
using System.Collections.Generic;
[System.Serializable]

public class City : Entity
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>The identifier.</value>
	public int id;
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>The name.</value>
	public string name;
	/// <summary>
	/// Gets or sets the description.
	/// </summary>
	/// <value>The description.</value>
	public string description;
	/// <summary>
	/// Gets or sets the type of the lock.
	/// </summary>
	/// <value>The type of the lock.</value>
	public string locktype;
	/// <summary>
	/// Gets or sets the condition.
	/// </summary>
	/// <value>The condition.</value>
	public int[] condition;

    public List<int> conditions;
    /// <summary>
	/// Gets or sets the coordinate x.
	/// </summary>
	/// <value>The coordinate x.</value>
	public int coordx;
	/// <summary>
	/// Gets or sets the coordinate y.
	/// </summary>
	/// <value>The coordinate y.</value>
	public int coordy;
	/// <summary>
	/// Gets or sets the building identifiers.
	/// </summary>
	/// <value>The building identifiers.</value>
	public int[] buildingid;

	/// <summary>
	/// The image.
	/// </summary>
	public string image;

	public List<Pair> coordinates;

	public int totalConditionCount;
	public int currentConditionCount;

	/// <summary>
	/// The event conditions in this city.
	/// </summary>
	public HashSet<int> eventConditions;
	/// <summary>
	/// The discovered event conditions in this city.
	/// </summary>
	public HashSet<int> discoveredEventConditions;

}

