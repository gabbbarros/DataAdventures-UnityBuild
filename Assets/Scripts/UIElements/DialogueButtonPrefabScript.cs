using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButtonPrefabScript : MonoBehaviour
{

    public DialogManager DLM;
    // Use this for initialization
    void Start()
    {
        DLM = GameObject.Find("GameManagers").GetComponent<DialogManager>();
    }

}
