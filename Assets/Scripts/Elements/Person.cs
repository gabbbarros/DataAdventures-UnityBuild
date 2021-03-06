﻿using System;
using System.Collections.Generic;
[System.Serializable]

public class Person : Entity
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
	/// Gets or sets the condition.
	/// </summary>
	/// <value>The condition.</value>
	public int[] condition;
	/// <summary>
	/// Gets or sets the building identifier.
	/// </summary>
	/// <value>The building identifier.</value>
	public int buildingid;
    /// <summary>
	/// Gets or sets the dialog rootid identifier.
	/// </summary>
	/// <value>The drootid identifier.</value>
	public int drootid;
	/// <summary>
	/// The image.
	/// </summary>
	public string image;
	/// <summary>
	public DNode rootnode;
	//public DialogueNode rootnode;
	public List<DNode> allNodes;
	//public List<DialogueNode> allNodes;


}

