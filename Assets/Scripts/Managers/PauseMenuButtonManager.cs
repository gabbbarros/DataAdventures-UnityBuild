using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenuButtonManager : MonoBehaviour {


	public Button[] buttons;
	public GameObject Menu;
	void Start()
	{
		buttons = this.GetComponentsInChildren<Button>();
	}
	public void DisableButtons()
	{
		foreach (Button b in buttons)
		{
			b.enabled = false;
		}
	}

	public void EnableButtons()
	{
		foreach (Button b in buttons)
		{
			b.enabled = true;
		}	
	}

	public void Unpause()
	{
		Menu.SetActive(false);
	}
}
