using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class FileReader : MonoBehaviour {

	void Start()
	{
		ReadSaveFile();
	}
	/// <summary>
	/// Gets or sets the name of the file.
	/// </summary>
	/// <value>The name of the file.</value>
    public string FileName{ get;set; }

    public static GameFile TheGameFile;

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
		string gFile = File.ReadAllText("Assets/Test.json");
		Debug.Log(gFile);
		//StringReader r1 = new StringReader(gF);
		//GameFile gf = JsonUtility.FromJson<GameFile>(GameFile);
		TheGameFile = JsonUtility.FromJson<GameFile>(gFile);
    }


}
