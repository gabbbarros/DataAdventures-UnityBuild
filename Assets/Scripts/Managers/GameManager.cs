using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public FileReader FileReaderManager;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
		FileReaderManager.ReadSaveFile();
		int max = FileReader.TheGameFile.InitConditions();
		FileReaderManager.SetUpSuspects();
		// set up condition manager
		ConditionManager cm = ConditionManager.GetInstance();

		cm.Initialize(FileReader.TheGameFile.ConditionSize);
		Debug.Log(FileReader.TheGameFile.people[0].condition[0]);

	}
}
