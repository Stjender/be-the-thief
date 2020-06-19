using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator;
    public bool lockedDoor;
    private bool open;

    public void openDoor()
    {
        if (!open)
        {
            doorAnimator.SetTrigger("openDoor");
            open = true;
        }
        else
        {
            doorAnimator.ResetTrigger("openDoor");
            open = false;
        }
    }
}
