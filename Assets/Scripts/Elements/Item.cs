using System;
[System.Serializable]
public class Item
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
	/// Gets or sets the event.
	/// </summary>
	/// <value>The event.</value>
	public int eventid;
	/// <summary>
	/// The image.
	/// </summary>
	public string image;
	/// <summary>
	/// Gets or sets the building identifier.
	/// </summary>
	/// <value>The building identifier.</value>
	public int buildingid;

	/// <summary>
	/// whether or not this item has been used. default is false.
	/// </summary>
	public bool used = false;
	/// <summary>
	/// Initializes a new instance of the <see cref="T:Item"/> class.
	/// </summary>
	public Item()
	{
		
	}
}

