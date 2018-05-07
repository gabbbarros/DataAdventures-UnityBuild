using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour {
    public Transform canvas;

    public GameObject Menu;
    public GameObject SettingsMenu;

    public GameObject PlaceHolder;

	public TutorialManager TM;
    private Animator anim;
	private Button[] buttons;


    void Start() {
        Time.timeScale = 1;
        anim = PlaceHolder.GetComponent<Animator>();
		buttons = PlaceHolder.GetComponentsInChildren<Button>();
        //disable it on start to stop it from playing the default animation
        anim.enabled = false;
        print("Starting");
    }

	// Update is called once per frame
	void Update () {
	    /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Menu.gameObject.activeInHierarchy == false)
            {
                Menu.gameObject.SetActive(true);
            }
            else
            {
                Menu.gameObject.SetActive(false);

            }

        }*/
	}

    public void Resume ()
    {
        //play the SlideOut animation
        anim.Play("PauseMenuSlideOut");
        //set back the time scale to normal time scale
        Time.timeScale = 1;
        
        //Menu.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu ()
    {
        Time.timeScale = 1;
		SceneManager.LoadScene("Intro");
    }

	public void StartTutorial()
	{
		Resume();
		TM.RestartTutorial();
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
        //enable the animator component
        anim.enabled = true;
        //play the Slidein animation
        anim.Play("PauseMenuSlideIn");
        //set the isPaused flag to true to indicate that the game is paused
        //freeze the timescale
        Time.timeScale = 0;
        
        /* print("here");
             //  MovablePart.GetComponent<RectTransform>().transform.Translate(Vector3.left * 100 * Time.deltaTime);
                MovablePart.GetComponent<RectTransform>().transform.position = Vector3.MoveTowards(
               MovablePart.GetComponent<RectTransform>().transform.position,
                    new Vector3(81.04f, -124.05F, 0), 
                     5 * Time.deltaTime);*/
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
}
