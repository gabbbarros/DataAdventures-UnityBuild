using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DNode
{
	public int id;
	public int dialoguetype;
	public int eventid = -1;
	public int[] condition;
	public bool visited = false;
	public string dialogueline;
	public string option;
	public bool isroot;
	public string[] keywords;
	public string[] keywordsList;
	public List<DNode> childrenNodes;
	public int[] children;
	public bool isVisited = false;

	public DNode(int id, int dialoguetype, int eventid, int[] condition, bool visited, string dialogueline,
				 string option, bool isroot, string[] keywords, string[] keywordsList, int[] children, bool isVisited)
	{
		this.id = id;
		this.dialoguetype = dialoguetype;
		this.eventid = eventid;
		this.condition = condition;
		this.visited = visited;
		this.dialogueline = dialogueline;
		this.option = option;
		this.isroot = isroot;
		this.keywords = keywords;
		this.keywordsList = keywordsList;
		this.children = children;
		this.isVisited = isVisited;
		childrenNodes = new List<DNode>();
	}
}

