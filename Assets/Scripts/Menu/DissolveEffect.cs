using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menuspace
{
    public class DissolveEffect : MonoBehaviour
    {
        Material material;

        public void Dissolve(float speed)
        {
            StartCoroutine(DissolvingCoroutine(speed));
        }

        IEnumerator DissolvingCoroutine(float speed)
        {
            float fade = 1;
            while (fade > 0)
            {
                fade -= speed * Time.deltaTime;
                material.SetFloat("_Fade", fade);
                yield return null;
            }
        }

        private void Awake()
        {
            material = GetComponent<SpriteRenderer>().material;
        }
    }

}