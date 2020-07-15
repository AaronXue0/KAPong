using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public class DoAbility : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public GameObject copyBall;

        private int anglepass = 0;
        private Color32 color;
        private bool shineUp = false;
        private bool isSin = false;
        private bool sinup = false;

        public float DoSpin(float angle, float spinSpeed)
        {
            angle += spinSpeed;
            return angle;
        }
        public void DoShine()
        {
            if (color.a >= 255)
            {
                shineUp = false;
            }
            if (color.a <= 1)
            {
                shineUp = true;
            }
            if (shineUp)
            {
                color.a += 5;
            }
            else
            {
                color.a -= 5;
            }
            spriteRenderer.color = color;
        }
        public float DoCopy(Vector2 nowSpot, float angle)
        {
            float sepAngle = Random.Range(-45, 45);
            Instantiate(copyBall, nowSpot, Quaternion.Euler(0, 0, angle - sepAngle));
            angle += sepAngle;
            return angle;
        }
        /* public void DoSin()
         {
             isSin = true;
         }*/
        public void DoSin(ref float angle)
        {
            if (sinup == true)
            {
                angle += 6;
                anglepass += 6;
            }
            if (sinup == false)
            {
                angle -= 6;
                anglepass -= 6;
            }
            if (anglepass <= 0)
            {
                sinup = true;
            }
            if (anglepass >= 180)
            {
                sinup = false;
            }

        }
        public void ResetAnlgePass()
        {
            anglepass = 0;
        }
        void Start()
        {
            color.r = 255;
            color.g = 255;
            color.b = 255;
            color.a = 255;
        }
    }
}