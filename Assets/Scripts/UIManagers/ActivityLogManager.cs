using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Globalization;
using System.IO;

public class ActivityLogManager : MonoBehaviour {

	// Log prefab for the Overview Tab
	public GameObject OverviewLogPrefab;
	// Log prefab for the Activity Log Tab
	public GameObject ActivityLogPrefab;

	// GameObject parent of Overview Logs
	public GameObject OverviewContent;
	// GameObject parent for Activity Logs
	public GameObject ActivityLogContent;

	public ScrollRect OverviewScrollRect;
	public ScrollRect ActivityLogScrollRect;

	public int BuildingVisitCount;
	public int PersonVisitCount;
	public int ItemInteractionCount;
	public int CityTravelCount;
	public int DialogueCount;

	public int JournalQueryCount;
	public int JournalItemQueryCount;
	public int JournalPersonQueryCount;
	public int JournalCityQueryCount;

	public string uniqueFileName;

	/// <summary>
	/// The List<string> that we can flush to file every once and a while
	/// </summary>
	public static List<string> logAllDis;


	private bool logging = false;
	void Start()
	{
		BuildingVisitCount = 0;
		PersonVisitCount = 0;
		ItemInteractionCount = 0;
		CityTravelCount = 0;
		DialogueCount = 0;
	}
	/// <summary>
	/// Inits the logging. We call this for every game to create a unique game file using the date and time.
	/// </summary>
	public void InitLogging()
	{
		if (logging)
		{
			DateTime localDate = DateTime.Now;
			uniqueFileName = string.Format(@"{0}.txt", localDate.ToString("s"));
			string path = "Assets/Resources/" + uniqueFileName;

			logAllDis = new List<string>();
			File.Create(uniqueFileName);
		}
	}

	[MenuItem("Tools/Write file")]

	// Adds a log object with the given param content to the Activity Log on
	// both the Overview Tab and the Activity Log Tab
	public void AddLog(string log)
	{
		Canvas.ForceUpdateCanvases();
		AddActivityLog(log);
		//AddOverviewLog(log);
		StartCoroutine(UpdateRoutine());
	}

	/// <summary>
	/// Adds the log to the list to be flushed to file later
	/// </summary>
	/// <param name="log">Log.</param>
	public void AddFileLog(string log)
	{
		if (logging)
		{
			logAllDis.Add(log);
			if (logAllDis.Count > 20)
			{
				FlushLog();
			}
		}
	}

	/// <summary>
	/// Flushs the log list to file and clears it for more space
	/// </summary>
	public void FlushLog()
	{
		if (logging)
		{
			LogMe(logAllDis);
			logAllDis.Clear();
		}
	}

	/// <summary>
	/// Adds a log to the activity log.
	/// </summary>
	/// <param name="log">Log.</param>
	public void AddActivityLog(string log)
	{
		// create the new log
		GameObject NewLog = Instantiate(ActivityLogPrefab, ActivityLogContent.transform);
		// change the text to match the param input
		NewLog.GetComponentInChildren<Text>().text = log;
	}

	/// <summary>
	/// Adds a log to the overview log.
	/// </summary>
	/// <param name="log">Log.</param>
	public void AddOverviewLog(string log)
	{
		// create the new log
		GameObject NewLog = Instantiate(OverviewLogPrefab, OverviewContent.transform);
		// change the text to match the param input
		NewLog.GetComponentInChildren<Text>().text = log;
	}

	/// <summary>
	/// Update routine for logs on the canvas to have proper auto scrolling.
	/// </summary>
	/// <returns>The routine.</returns>
	IEnumerator UpdateRoutine()
	{
		yield return new WaitForEndOfFrame();
		Canvas.ForceUpdateCanvases();
//		OverviewScrollRect.verticalScrollbar.value = 0f;
		ActivityLogScrollRect.verticalScrollbar.value = 0f;
		Canvas.ForceUpdateCanvases();
	}

	/// <summary>
	/// Logs the given log list to the game file.
	/// </summary>
	/// <param name="log">Log.</param>
	public void LogMe(List<string> log)
	{
		string path = "Assets/Resources/" + uniqueFileName;
		using (StreamWriter writer =
		   new StreamWriter(path, true))
		{
			foreach (string line in log)
			{
				writer.WriteLine(line);
			}
		}
		//Re-import the file to update the reference in the editor
		AssetDatabase.ImportAsset(path);
		TextAsset asset = Resources.Load("test") as TextAsset;
	}


	//static void WriteString()
	//{
	//	string path = ;

	//	//Write some text to the test.txt file
	//	StreamWriter writer = new StreamWriter(path, true);
	//	writer.WriteLine("Test");
	//	writer.Close();



	//	//Print the text from the file
	//	//Debug.Log(asset.text); 	
	//}
}
