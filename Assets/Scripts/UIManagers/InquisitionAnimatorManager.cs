using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InquisitionAnimatorManager : MonoBehaviour {

	public GameManager GM;

	public void CanIStartAnInquisition()
	{
		GM.IQM.DisplayFact();
	}
}
