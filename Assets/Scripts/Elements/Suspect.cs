using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Suspect
{
	public Person me;
	public List<string> Facts;

	public int[] factSpeculation;
	public Suspect(int factCount, Person me)
	{
		factSpeculation = new int[factCount];
		for (int i = 0; i < factSpeculation.Length; i++)
		{
			factSpeculation[i] = -1;
		}
		this.me = me;
	}

}

