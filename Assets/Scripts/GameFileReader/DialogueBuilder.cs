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

	List<DialogueNode> BuildTrees(List<DialogueNode> nodes) {
		List<DialogueNode> roots = new List<DialogueNode>();

		for(int i = 0; i < nodes.Count; i++) {
			DialogueNode node = nodes.Items[i];
			for(int j = 0; j < nodes.Count; j++) {
				if(Contains(node.children, nodes.Items[j].id)) {
					node.childrenNodes.Add(nodes.Items[j]);
				}
			}
		}

		foreach(DialogueNode d in nodes) {
			if(d.isRoot) {
				roots.Add(d);
			}
		}

		return roots;
	}
}
