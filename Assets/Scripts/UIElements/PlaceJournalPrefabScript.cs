using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceJournalPrefabScript : MonoBehaviour
{
    public City City;
    public Text DescriptionTitle;
    public Text DescriptionContent;

    void Start()
    {
     	DescriptionTitle = GameObject.Find("Description Title").GetComponent<Text>();
		DescriptionContent = GameObject.Find("Description Content").GetComponent<Text>();   
    }
}
