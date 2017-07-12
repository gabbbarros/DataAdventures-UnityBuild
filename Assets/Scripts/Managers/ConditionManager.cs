using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : Singleton<ConditionManager> {
	public BitArray conditions;

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
		if(bitIndex >= 0)
			conditions.Set(bitIndex, true);
	}
	
	public void SetAsFalse(int bitIndex) {
		if(bitIndex >= 0)
			conditions.Set(bitIndex, false);
	}
	
	public void ClearConditions() {
		conditions.SetAll(false);
	}
}
