using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueButtonPrefabScript : MonoBehaviour
{

    public DialogManager DLM;
    public Text Line;
    public DialogueNode me;
    // Use this for initialization
    void Start()
    {
        DLM = GameObject.Find("GameManagers").GetComponent<DialogManager>();
    }

    public void SetLine(string l)
    {
        Line.text = l;
    }
    
    /// <summary>
    /// Populates the list with all dialogue choice children when pressed
    /// </summary>
    public void Clicked()
    {
        DLM.ClearOptions();
        
        // loop through all children and add to dialogue choices
        foreach(DialogueNode child in me.childrenNodes)
        {
            DLM.AddChoice(child);
        }
    }

}
