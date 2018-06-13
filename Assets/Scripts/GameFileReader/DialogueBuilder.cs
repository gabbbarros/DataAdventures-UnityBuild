using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBuilder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	bool Contains(int[] items, int number) {
		foreach(int i in items) {
			if(i == number)
				return true;
		}
		return false;
	}

	/*
	Builds all dialogue trees. Return a list of root nodes
 	*/
	public List<DNode> BuildTrees(DialogueNode[] nodesList) {
		List<DNode> roots = new List<DNode>();
		List<DNode> tempList = new List<DNode>();
		DNode[] nodes = new DNode[nodesList.Length];
		foreach(DialogueNode node in nodesList)
		{
			DNode nodee = new DNode(node.id, node.dialoguetype, node.eventid, node.condition, node.visited, node.dialogueline, node.option, node.isroot, node.keywords, node.keywordsList, node.children, node.isVisited);
			tempList.Add(nodee);
		}
		nodes = tempList.ToArray();

		for(int i = 0; i < nodes.Length; i++) {
			DNode node = nodes[i];
			// turn every DialogueNode into a DNode
			for(int j = 0; j < nodes.Length; j++) {
				if(Contains(node.children, nodes[j].id)) {
					node.childrenNodes.Add(nodes[j]);
				}
			}
		}

		foreach(DNode d in nodes) {
			if(d.isroot) {
				roots.Add(d);
			}
		}
		return roots;
	}

	public void AddDialogueRootToPeople(List<Person> people, List<DNode> roots) {
		foreach(Person p in people) {
			foreach(DNode root in roots) {
				if(root.id == p.drootid) {
					p.rootnode = root;
					List<DNode> nodes = BFS(root);
					p.allNodes = nodes;
					break;
				}
			}
		}
	}

	public List<DNode> BFS(DNode root)
	{
		List<DNode> nodes = new List<DNode>();

		List<DNode> queue = new List<DNode>();
		queue.Add(root);
		while (queue.Count > 0)
		{
			DNode current = queue[0];
			nodes.Add(current);
			queue.RemoveAt(0);
			foreach (DNode child in current.childrenNodes)
			{
				if (!nodes.Contains(child))
				{
					queue.Add(child);
				}
			}
		}

		return nodes;
	}
}
