using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public class Ability : MonoBehaviour
    {
        [Header("Abilities: Sin Wave")]
        [SerializeField]
        public bool sinWave = false;
        public bool SinWave { get { return sinWave; } }
        [SerializeField]
        public bool separate = false;
        public bool Separate { get { return separate; } }
        [SerializeField]
        public bool spin = false;
        public bool Spin { get { return spin; } }
        [SerializeField]
        float waveMagnitude = 0.5f;
        public float WaveMagnitude { get { return waveMagnitude; } }
        [SerializeField]
        float waveFrequency = 20f;
        public float WaveFrequency { get { return waveFrequency; } }

        [Header("Abilities: Transparency")]
        [SerializeField]
        public bool transparency = false;
        public bool Transparency { get { return transparency; } }
        [SerializeField]
        byte increaseValue = 5;
        public byte IncreaseValue { get { return increaseValue; } }
        [SerializeField]
        byte decreaseValue = 5;
        public byte DecreaseValue { get { return decreaseValue; } }
        [SerializeField]
        bool isDecreasing = true;
        public bool IsDecreasing { get { return isDecreasing; } }
        [SerializeField]
        Color32 color = Color.white;
        public Color32 BallColor { get { return color; } }

        [Header("Abilities: Spin")]
        [SerializeField]
        GameObject separateball;
        [SerializeField]
        float HowMuchSpinTime;
        float countSpinTime;
        
        FireBall ball;

        void Start()
        {
            ball = GetComponent<FireBall>();
        }
    }
}