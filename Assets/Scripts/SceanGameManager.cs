using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanGameManager : MonoBehaviour {

	public void MoveBetweenSceans (string gameScean)
	{
		SceneManager.LoadScene (gameScean);
	}
}
