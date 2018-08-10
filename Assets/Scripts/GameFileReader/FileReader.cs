using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
public class FileReader : MonoBehaviour
{

    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    /// <value>The name of the file.</value>
    public string FileName { get; set; }

    public static GameFile TheGameFile;

    public SuspectListManager SLM;


    public static bool isDemo = false;

    /// <summary>
    /// Reads the save file.
    /// </summary>
    public void ReadSaveFile()
    {
        string result = "";

        string filename = Path.Combine(Application.streamingAssetsPath, StaticGameInfo.GameName + ".json");

        if (isDemo)
        {
            TextAsset file = Resources.Load(StaticGameInfo.GameName) as TextAsset;
            Debug.Log(StaticGameInfo.GameName + ".json");
            result = file.text;
        }
        else
        {
            if (filename.Contains("://") || filename.Contains(":///"))
            {
                WWW www = new WWW(filename);
                result = www.text;
            }
            else
            {
                result = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, StaticGameInfo.GameName + ".json"));
            }

        }

        //TextAsset gAsset = Resources.Load("Albert_Einstein") as TextAsset;
        //Debug.Log
        //string gFile = gAsset.text;
        //Debug.Log(gFile);
        //StringReader r1 = new StringReader(gF);
        //GameFile gf = JsonUtility.FromJson<GameFile>(GameFile);
        TheGameFile = JsonUtility.FromJson<GameFile>(result);
		TheGameFile.Keys = new List<Item>();
		TheGameFile.Lights = new List<Item>();
		TheGameFile.Crowbars = new List<Item>();
		TheGameFile.Locks = new List<Building>();
		TheGameFile.Darks = new List<Building>();
		TheGameFile.Chains = new List<Building>();

		// setup keywords list
		//foreach (DialogueNode p in TheGameFile.dialoguenodes)
		//{
		//	Debug.Log(p.keywords);
		//}
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

    public Sprite[] LoadSprites() {
        Sprite[] Images;
        if(isDemo) {
            Images = Resources.LoadAll<Sprite>(StaticGameInfo.GameName + "/");

        } else{
            string path = Path.Combine(Application.streamingAssetsPath, StaticGameInfo.GameName);
            Debug.Log(path);
            var info = new DirectoryInfo(path);
            var fileInfo = info.GetFiles();

            List<Sprite> temp = new List<Sprite> ();
            foreach(FileInfo file in fileInfo) {
                if (!file.Name.Contains(".meta") && file.Name.Contains(".png")) {
                    //WWW www = new WWW(file.FullName);
                    //while(!www.isDone) {
                    //    Debug.Log("not done");
                    //}
                    //Debug.Log(www);
                    //Texture2D texture = www.texture;
                    //Debug.Log(texture);

                    //Texture2D texture = new WWW(file.FullName).texture;
                    Sprite sprite = Utilities.LoadNewSprite(file.FullName);
                    sprite.name = file.Name;
                    //Debug.Log(file.Name);
                    temp.Add(sprite);
                }
            }
            Images = temp.ToArray();


        }

        foreach (Sprite s in Images)
        {
            Debug.Log(s.name);

        }
        
        return Images;
    }
}
