using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleJournalPrefabScript : MonoBehaviour
{
    public Person Person;
    public Text DescriptionTitle;
    public Text DescriptionContent;

	void Start()
	{
		DescriptionTitle = GameObject.Find("Description Title").GetComponent<Text>();
		DescriptionContent = GameObject.Find("Description Content").GetComponent<Text>();
	}
}
