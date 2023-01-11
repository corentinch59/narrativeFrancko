using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    public void PlayPrepare()
    {
        animator.Play("Dual_Prepare");
    }

    public void PlayReady()
    {
        animator.Play("Dual_Ready");
    }

    public void PlayShoot()
    {
        animator.Play("Dual_Shoot");
    }

    public void PlayKnifeTravel()
    {
        animator.Play("Dual_KnifeTravel");
    }

    public void PlayShooting()
    {
        animator.Play("Dual_Shooting");
    }

    public void PlayEnd()
    {
        animator.Play("Dual_End");
    }
}
