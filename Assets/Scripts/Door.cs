﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDoor()
    {
        doorAnimator.SetTrigger("openDoor");
        doorAnimator.ResetTrigger("openDoor");
    }
}
