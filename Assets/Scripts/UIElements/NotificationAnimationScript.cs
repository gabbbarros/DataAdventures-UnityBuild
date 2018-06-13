using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationAnimationScript : MonoBehaviour {

    public Animator A;
	public GameObject Point;
    void Start()
    {
        A = gameObject.GetComponent<Animator>();
    }
    public void StopAnimation()
    {
        A.Play("Static");
    }

	public void NewInformation()
	{
		Point.SetActive(true);
	}

	public void StopInformation()
	{
		Point.SetActive(false);
	}
}
