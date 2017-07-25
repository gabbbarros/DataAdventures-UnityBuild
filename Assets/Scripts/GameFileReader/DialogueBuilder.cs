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
	public List<DialogueNode> BuildTrees(DialogueNode[] nodes) {
		List<DialogueNode> roots = new List<DialogueNode>();

		for(int i = 0; i < nodes.Length; i++) {
			DialogueNode node = nodes[i];
			for(int j = 0; j < nodes.Length; j++) {
				if(Contains(node.children, nodes[j].id)) {
					node.childrenNodes.Add(nodes[j]);
				}
			}
		}

		foreach(DialogueNode d in nodes) {
			if(d.isroot) {
				roots.Add(d);
			}
		}

		return roots;
	}

	public void AddDialogueRootToPeople(List<Person> people, List<DialogueNode> roots) {
		foreach(Person p in people) {
			foreach(DialogueNode root in roots) {
				if(root.id == p.drootid) {
					p.rootnode = root;
					break;
				}
			}
		}
	}
}
