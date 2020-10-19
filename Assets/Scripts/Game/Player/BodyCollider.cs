using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.Playerspace
{
    public class BodyCollider : MonoBehaviour
    {
        Player player;
        private void Awake()
        {
            player = GetComponentInParent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Ball") player.Hurt(1);
        }
    }
}