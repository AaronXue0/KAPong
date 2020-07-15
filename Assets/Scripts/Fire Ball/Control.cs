using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public class Control
    {
        public void DoMove(Rigidbody2D rb, Vector2 force)
        {
            rb.velocity = force;
        }
        public void BounceHandling(ref float speed, ref Vector2 movement, Transform transform, Collider2D other)
        {
            switch (other.tag)
            {
                case "Player":
                    movement = transform.position - other.transform.position;
                    float distance = movement.magnitude;
                    speed = 10 / (Mathf.Abs(distance - 2) + 1);
                    break;
            }
        }
        public void BounceHandling(ref float speed, ref Vector2 movement, Rigidbody2D rb, Transform transform, Collision2D other)
        {
            Vector2 inDirection = movement;
            Vector2 inNormal = other.contacts[0].normal;
            movement = Vector2.Reflect(inDirection, inNormal);
        }
    }

}