using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class DialogueNode {
	public int id;
	public int dialoguetype;
	public int eventid;
	public int[] condition;
	public bool visited = false;
	public string dialogueline;
	public string option;
	public bool isRoot;
	public List<DialogueNode> childrenNodes;
	public int[] children;

}
