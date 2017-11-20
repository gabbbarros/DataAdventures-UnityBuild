using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class FileReader : MonoBehaviour {

	/// <summary>
	/// Gets or sets the name of the file.
	/// </summary>
	/// <value>The name of the file.</value>
	public string FileName { get; set; }

	public static GameFile TheGameFile;

	public SuspectListManager SLM;

	/// <summary>
	/// Reads a SaveGame file. SaveGame files are saved in JSON format
	/// </summary>
    public FileReader()
    {

    }
	/// <summary>
	/// Reads the save file.
	/// </summary>
    public void ReadSaveFile()
    {
		string gFile = File.ReadAllText("Games/Albert_Einstein-0.json");
		//TextAsset gAsset = Resources.Load("Albert_Einstein") as TextAsset;
		//Debug.Log
		//string gFile = gAsset.text;
		//Debug.Log(gFile);
		//StringReader r1 = new StringReader(gF);
		//GameFile gf = JsonUtility.FromJson<GameFile>(GameFile);
		TheGameFile = JsonUtility.FromJson<GameFile>(gFile);
		TheGameFile.Keys = new List<Item>();
		TheGameFile.Lights = new List<Item>();
		TheGameFile.Crowbars = new List<Item>();
		TheGameFile.Locks = new List<Building>();
		TheGameFile.Darks = new List<Building>();
		TheGameFile.Chains = new List<Building>();

		// setup keywords list
		foreach (DialogueNode p in TheGameFile.dialoguenodes)
		{
			Debug.Log(p.keywords);

		}
		//// setup keywords list
		//foreach (DialogueNode p in TheGameFile.dialoguenodes)
		//{
		//	Debug.Log(p.keywords);
		//	p.SetUpKeywordsList();
		//	Debug.Log(p.keywordsList);
		//}
    }

	public void SetUpSuspects()
	{
		foreach (int ID in TheGameFile.crime.suspects)
		{
			//Debug.Log(ID);
			Person addMe = new Person();
			foreach (Person p in TheGameFile.people)
			{
				if (p.id == ID)
				{
					addMe = p;
				}
			}
			SLM.AddSuspect(addMe);
		}

		TheGameFile.SetUpSuspects();
	}

	public void SetUpLockDarkAndKey()
	{
		foreach (Building b in TheGameFile.buildings)
		{
			if (b.locktype.Equals("dark"))
			{
				TheGameFile.Darks.Add(b);
			}
			else if (b.locktype.Equals("lock"))
			{
				TheGameFile.Locks.Add(b);
			}
			else if (b.locktype.Equals("chain"))
			{
				TheGameFile.Chains.Add(b);
			}
		}

		foreach (Item i in TheGameFile.items)
		{
			if (i.name.Equals("Key"))
			{
				TheGameFile.Keys.Add(i);
			}
			else if (i.name.Equals("Flashlight"))
			{
				TheGameFile.Lights.Add(i);
			}
			else if (i.name.Equals("Crowbar"))
			{
				TheGameFile.Crowbars.Add(i);
			}
		}
	}
}
