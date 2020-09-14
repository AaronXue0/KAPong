using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class AudioDJ : MonoBehaviour
    {
        public AudioClip[] clips;

        AudioSource audioSource;

        public void SetcionA()
        {
            StartCoroutine(nextPlay(clips[2], clips[3], true, 0.6f));
        }
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            StartCoroutine(nextPlay(clips[0], clips[1], true, 1));
        }
        IEnumerator nextPlay(AudioClip clipA, AudioClip clipB, bool loop, float volume)
        {
            audioSource.clip = clipA;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            audioSource.clip = clipB;
            audioSource.loop = loop;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}