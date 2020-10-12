using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public class Control
    {
        public void BounceHandling(ref float speed, ref Vector2 movement, Transform transform, Collider2D other)
        {
            switch (other.tag)
            {
                case "Player":
                    if (Mathf.Abs(movement.x) <= 0.2f) return;
                    movement = new Vector2(0.2f, 0);
                    break;
                case "Player Sword":
                    movement = transform.position - other.transform.position;
                    speed = movement.magnitude;
                    break;
            }
        }
        public void BounceHandling(ref float speed, ref Vector2 movement, Transform transform, Collision2D other)
        {
            switch (other.collider.tag)
            {
                default:
                    Vector2 inDirection = movement;
                    Vector2 inNormal = other.contacts[0].normal;
                    movement = Vector2.Reflect(inDirection, inNormal);
                    break;
            }
        }
    }
}