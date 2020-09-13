using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.Playerspace
{
    public class Control : MonoBehaviour
    {
        [SerializeField]
        float hurtDuration = 1f;
        [SerializeField]
        public Transform checkPos;
        [SerializeField]
        public float radius;
        [SerializeField]
        public LayerMask whatIsBorder;

        bool isHurt = false;
        public bool IsHurt { get { return isHurt; } }

        public void BorderHandling(ref Vector2 movement)
        {
            Vector2 pos = new Vector2(checkPos.position.x, checkPos.position.y) + movement;
            if (Physics2D.OverlapCircle(pos, radius, whatIsBorder))
            {
                Vector2 posX = new Vector2(movement.x + checkPos.position.x, 0);
                Vector2 posY = new Vector2(0, movement.y + checkPos.position.y);
                if (Physics2D.OverlapCircle(posX, radius, whatIsBorder)) movement -= new Vector2(movement.x, 0);
                if (Physics2D.OverlapCircle(posY, radius, whatIsBorder)) movement -= new Vector2(0, movement.y);
            }
        }

        public void HurtHandling()
        {
            isHurt = true;
            Invoke("SetHurtFalse", hurtDuration);
        }

        void SetHurtFalse()
        {
            isHurt = false;
        }
    }
}