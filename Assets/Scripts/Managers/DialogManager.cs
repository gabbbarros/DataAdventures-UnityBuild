using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	/// <summary>
	/// The options.
	/// </summary>
	public GameObject Options;

	/// <summary>
	/// The name.
	/// </summary>
	public Text Name;
	/// <summary>
	/// The dialogue the person said.
	/// </summary>
	public Text Dialogue;

    /// <summary>
    /// The image of the person.
    /// </summary>
    public Image Photo;


    public GameObject DialogueButtonPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadPerson(Person me)
	{
        // display person name
        Name.text = me.name;

        // display person image
        // TODO : (do it here)

        // display dialogue line at beginning
        Debug.Log(me.rootnode.id + ": " + me.rootnode.dialogueline);
        Dialogue.text = me.rootnode.dialogueline;

        // lay down the choices
        foreach(DialogueNode child in me.rootnode.childrenNodes)
        {
            GameObject choice = Instantiate(DialogueButtonPrefab, Options.transform);
            
        }
	}

	
}
