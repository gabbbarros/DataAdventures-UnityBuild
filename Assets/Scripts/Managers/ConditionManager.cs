using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : Singleton<ConditionManager> {
	public BitArray conditions;

	public JournalManager JM;

	// Use this for initialization
	void Start () {
		//instance = null;
		//ConditionManager test = ConditionManager.GetInstance();
		//test.Initialize(4);
		//test.conditions.Set(2,true);

		//string s = "";
		//for(int i  = 0; i < test.conditions.Count; i++) {
		//	s+=test.conditions.Get(i)+"\t";
		//}
		//Debug.Log(s);
		//Debug.Log(test.conditions.ToString());	
		JM = GameObject.FindWithTag("UI Manager").GetComponent<JournalManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*static ConditionManager getInstance() {
		if(instance == null)
			instance = new ConditionManager();
			return instance;
	}*/

	public void Initialize(int amountConditions) {
		conditions = new BitArray(amountConditions + 1);
		Debug.Log("Conditions Size: " + conditions.Count);
	}

	/**
	Return true is condition is positive (meaning it was activated)
 	*/
	public bool IsSet(List<int> list) {
		foreach(int index in list) {
			if(index < 0 || this.conditions.Get(index))
				return true;
		}
		return false;
	}
	
	/**
	 * If active, has seen. 
	 Return true is condition is positive (meaning it was activated)
	 * @param condition
	 * @return
	 */
	public bool IsSet(int condition) {
		if(condition < 0)
			return true;
		return this.conditions.Get(condition);
	}
	
	public void SetAsTrue(int bitIndex) {
		if (bitIndex >= 0)
		{
			Debug.Log("Index:" + bitIndex);
			bool wasFalse = !IsSet(bitIndex);
			conditions.Set(bitIndex, true);
			if(wasFalse)
				TrollEverything(bitIndex);
		}
	}
	
	public void SetAsFalse(int bitIndex) {
		if(bitIndex >= 0)
			conditions.Set(bitIndex, false);
	}
	
	public void ClearConditions() {
		conditions.SetAll(false);
	}

	// this method handles all new flag set to true operations.
	// if an event is set to true, all cities will be notified in the journal
	public void TrollEverything(int condition)
	{
		City[] cities = FileReader.TheGameFile.cities;
		foreach (City me in cities)
		{
			int[] conditions = me.condition;
			foreach (int cond in conditions)
			{
				if (cond == condition)
				{
					// this is one of the cities we must make a notification for
					JM.AddPlace(me, true);
					break;
				}
			}

		}
	}
}
