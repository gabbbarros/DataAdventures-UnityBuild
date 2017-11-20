using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	void Start()
	{
	}
	// Adds a log object with the given param content to the Activity Log on
	// both the Overview Tab and the Activity Log Tab
	public void AddLog(string log)
	{
		Canvas.ForceUpdateCanvases();
		AddActivityLog(log);
		//AddOverviewLog(log);
		StartCoroutine(UpdateRoutine());
	}

	public void AddActivityLog(string log)
	{
		// create the new log
		GameObject NewLog = Instantiate(ActivityLogPrefab, ActivityLogContent.transform);
		// change the text to match the param input
		NewLog.GetComponentInChildren<Text>().text = log;
	}

	public void AddOverviewLog(string log)
	{
		// create the new log
		GameObject NewLog = Instantiate(OverviewLogPrefab, OverviewContent.transform);
		// change the text to match the param input
		NewLog.GetComponentInChildren<Text>().text = log;
	}
	IEnumerator UpdateRoutine()
	{
		yield return new WaitForEndOfFrame();
		Canvas.ForceUpdateCanvases();
//		OverviewScrollRect.verticalScrollbar.value = 0f;
		ActivityLogScrollRect.verticalScrollbar.value = 0f;
		Canvas.ForceUpdateCanvases();
	}

}
