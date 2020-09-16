using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.Playerspace
{
    public class Control : MonoBehaviour
    {
        [SerializeField]
        public SpriteRenderer[] hearts;
        [SerializeField]
        public Sprite[] heartsImage;
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

        public void Recovery()
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                StartCoroutine(SetHeartImage(hearts[i], heartsImage[0], i * 0.3f));
            }
        }

        public IEnumerator SetHeartImage(SpriteRenderer image, Sprite sprite, float delay)
        {
            yield return new WaitForSeconds(delay);
            image.sprite = sprite;
        }

        public void HurtHandling(int n)
        {
            isHurt = true;
            Invoke("SetHurtFalse", hurtDuration);
            if (n >= 0 && n < hearts.Length) hearts[n].sprite = heartsImage[1];
        }

        void SetHurtFalse()
        {
            isHurt = false;
        }
    }
}