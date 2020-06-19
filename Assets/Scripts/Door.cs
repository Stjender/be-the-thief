using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator;
    public bool lockedDoor;
    public bool open;

    public void openDoor()
    {
        if (!open && !doorAnimator.GetCurrentAnimatorStateInfo(0).IsTag("closing") || !doorAnimator.GetCurrentAnimatorStateInfo(0).IsTag("opening"))
        {
            doorAnimator.SetTrigger("openDoor");
            open = true;
        }
        else if(!doorAnimator.GetCurrentAnimatorStateInfo(0).IsTag("closing") || !doorAnimator.GetCurrentAnimatorStateInfo(0).IsTag("opening"))
        {
            doorAnimator.ResetTrigger("openDoor");
            open = false;
        }
    }
}
