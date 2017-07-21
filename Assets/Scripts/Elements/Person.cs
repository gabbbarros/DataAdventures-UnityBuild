using System;
[System.Serializable]

public class Person
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
	public DialogueNode rootnode;
	/// Initializes a new instance of the <see cref="T:Person"/> class.
	/// </summary>
	public Person()
	{
	}

}

