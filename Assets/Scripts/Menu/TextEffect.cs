using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menuspace
{
    public class TextEffect : MonoBehaviour
    {
        public float min;
        SpriteRenderer spriteRenderer;
        Color color = Color.white;
        bool isTransparency;

        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            color.a = 1;
        }

        void Flashing()
        {
            //StartCoroutine(FlashingCoroutine());
        }

        // Update is called once per frame
        void Update()
        {
            if (isTransparency)
            {
                if (color.a <= min) {
                    color.a = min;
                    isTransparency = !isTransparency;
                }
                color.a -= Time.deltaTime;
                spriteRenderer.color = color;
            }
            else
            {
                if (color.a >= 1) {
                    color.a = 1;
                    isTransparency = !isTransparency;
                }
                color.a += Time.deltaTime;
                spriteRenderer.color = color;
            }
        }
    }
}