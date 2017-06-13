using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode {
	public int id;
	public int type;
	public bool visited = false;
	public string line;
	public string option;
	public bool isRoot;
	public List<DialogueNode> childrenNodes;
	public int[] children;

}
