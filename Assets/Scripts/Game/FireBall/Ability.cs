using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public class Ability : MonoBehaviour
    {
        [Header("Abilities: Sine Wave")]
        [SerializeField]
        float waveFrequency = 10f;
        [SerializeField]
        float waveMagnitude = 1f;

        public Vector2 DoSineWave()
        {
            Vector3 pos = transform.right + transform.up * Mathf.Sin(Time.time * waveFrequency) * waveMagnitude;
            return pos;
        }
        public IEnumerator DoEarse(float duration, System.Action callback)
        {
            yield return new WaitForSeconds(duration);
            callback();
        }
    }
}