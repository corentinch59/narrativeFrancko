using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    public void PlayPrepare(bool smooth)
    {
        if (smooth)
            animator.SetTrigger("Prepare");
        else
            animator.Play("Dual_Prepare");
    }

    public void PlayReady(bool smooth)
    {
        if (smooth)
            animator.SetTrigger("Ready");
        else
            animator.Play("Dual_Ready");
    }

    public void PlayShoot(bool smooth)
    {
        if (smooth)
            animator.SetTrigger("Shoot");
        else
            animator.Play("Dual_Shoot");
    }

    public void PlayKnifeTravel(bool smooth)
    {
        if (smooth)
            animator.SetTrigger("KnifeTravel");
        else
            animator.Play("Dual_KnifeTravel");
    }

    public void PlayEnd(bool smooth)
    {
        if (smooth)
            animator.SetTrigger("End");
        else
            animator.Play("Dual_End");
    }
}
