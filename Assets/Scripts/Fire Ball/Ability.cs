using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public class Ability
    {
        public void DoSinWave(ref Vector2 movement, Transform transform, float waveFrequency, float waveMagnitude)
        {
            if (movement == Vector2.zero) return;
            Vector3 pos = transform.right;
            movement = pos + transform.up * Mathf.Sin(Time.time * waveFrequency) * waveMagnitude;
        }

        public void DoTransparency(ref Color32 color,byte value)
        {
            color.a -= value;
        }
        public void DoOpacity(ref Color32 color,byte value)
        {
            color.a += value;
        }
        public void DoSeparate(ref Vector2 movement, Transform transform,float angle)
        {
            
            movement = transform.right + transform.up * angle;
        }
        public void DoSpin(ref Vector2 movement, Transform transform,float angle)
        {
            movement = transform.right + transform.up * angle;
        }
    }
}