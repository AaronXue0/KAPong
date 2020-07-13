using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control
{
    public void DoMove(Rigidbody2D rb, Vector2 force)
    {
        rb.velocity = force;
    }
    
    public void DoAnimator(Animator animator, string name)
    {
        animator.SetTrigger(name);
    }
}
