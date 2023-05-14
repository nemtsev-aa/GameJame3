using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Gun
{
    public Animator Animator;

    public void SetGrip()
    {
        Animator.SetBool("Grip", true);
    }
    public void SetCast()
    {
        Animator.SetBool("Grip", false);
    }

}
