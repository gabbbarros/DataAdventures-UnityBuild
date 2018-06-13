using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingNameAdjuster : MonoBehaviour {

	void Start()
	{
		AdjustPosition();
	}

	void AdjustPosition()
	{
		if (transform.parent.localPosition.x > 110.8)
		{
			Debug.Log(transform.localPosition.x);
			this.transform.localPosition = new Vector2(this.transform.localPosition.x - (transform.parent.localPosition.x - 100f), this.transform.localPosition.y);
			Debug.Log(transform.localPosition.x);
		}

		if (transform.parent.localPosition.y > 71)
		{
			this.transform.localPosition = new Vector2(this.transform.localPosition.x, -1 * this.transform.localPosition.y - 5);
		}
	}
}
