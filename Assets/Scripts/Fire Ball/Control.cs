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
        public void DoMove(Transform transform, Vector2 force)
        {
            transform.position = transform.position + new Vector3(force.x, force.y, 0);
        }
        public void BounceHandling(ref float speed, ref Vector2 movement, Transform transform, Collider2D other)
        {
            switch (other.tag)
            {
                case "Player":
                    if(movement == Vector2.zero) return;
                    movement = new Vector2(movement.x > 0 ? -0.1f : 0.1f, 0);
                    break;
                case "Player Sword":
                    movement = transform.position - other.transform.position;
                    float distance = movement.magnitude;
                    speed = 10 / (Mathf.Abs(distance - 2) + 1);
                    break;
            }
        }
        public void BounceHandling(ref float speed, ref Vector2 movement, Rigidbody2D rb, Transform transform, Collision2D other)
        {
            switch (other.collider.tag)
            {
                case "Player":
                    if(movement == Vector2.zero) return;
                    movement = new Vector2(movement.x > 0 ? -0.1f : 0.1f, 0);
                    break;
                default:
                    Vector2 inDirection = movement;
                    Vector2 inNormal = other.contacts[0].normal;
                    movement = Vector2.Reflect(inDirection, inNormal);
                    break;
            }
        }
    }

}