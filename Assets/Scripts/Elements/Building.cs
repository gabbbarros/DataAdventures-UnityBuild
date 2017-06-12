using System;
[System.Serializable]

public class Building
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
	/// Gets or sets the condition.
	/// </summary>
	/// <value>The condition.</value>
	public int[] condition;
	/// <summary>
	/// Gets or sets the city identifier.
	/// </summary>
	/// <value>The city identifier.</value>
	public int cityid;
	/// <summary>
	/// Gets or sets the person identifiers.
	/// </summary>
	/// <value>The person identifiers.</value>
	public int[] peopleid;
	/// <summary>
	/// Gets or sets the item identifier.
	/// </summary>
	/// <value>The item identifier.</value>
	public int[] itemsid;

	/// <summary>
	/// Initializes a new instance of the <see cref="T:Building"/> class.
	/// </summary>
	public Building()
	{
		
	}
}

