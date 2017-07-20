using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationAnimationScript : MonoBehaviour {

    public Animator A;
    void Start()
    {
        A = gameObject.GetComponent<Animator>();
    }
    public void StopAnimation()
    {
        A.Play("Static");
    }
}
