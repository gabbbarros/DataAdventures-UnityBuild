using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
	protected DialogueNode root;

	/// <summary>
	/// The options.
	/// </summary>
	public List<DialogueNode> Options;

	/// <summary>
	/// The name.
	/// </summary>
	public Text Name;
	/// <summary>
	/// The dialogue the person said.
	/// </summary>
	public Text Dialogue;

	public Text Choice1;
	public Text Choice2;
	public Text Choice3;
	public Text Choice4;

	// Use this for initialization
	void Start () {
		Options = root.childrenNodes;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadPerson(Person me)
	{

	}
	
}
