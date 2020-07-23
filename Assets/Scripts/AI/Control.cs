using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.Enemy
{
    public class Control
    {
        public void DoMove(Rigidbody2D rb, Vector2 force)
        {
            rb.MovePosition(force);
        }
        public void DoAnimator(Animator animator, string name)
        {
            animator.SetTrigger(name);
        }
        public void DoAnimator(Animator animator, string name, float value)
        {
            animator.SetFloat(name, value);
        }
    }
}