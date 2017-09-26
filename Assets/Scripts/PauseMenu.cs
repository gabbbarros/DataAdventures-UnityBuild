using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public Transform canvas;

    public GameObject Menu;
    public GameObject SettingsMenu;

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Menu.gameObject.activeInHierarchy == false)
            {
                Menu.gameObject.SetActive(true);
            }
            else
            {
                Menu.gameObject.SetActive(false);

            }

        }
	}

    public void Resume ()
    {
        Menu.gameObject.SetActive(false);
  
    }
    public void ReturnToMainMenu ()
    {
        
    }
    public void Settings ()
    {
        Menu.gameObject.SetActive(false);
        SettingsMenu.gameObject.SetActive(true); 
    }
    public void SettingsOkay ()
    {
        SettingsMenu.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
    }
    public void SettingsCancel ()
    {
        SettingsMenu.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true); 
    }

	public void Pause()
	{

        Menu.gameObject.SetActive(true);
	}
}
