using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAudio : MonoBehaviour
{
    public AudioClip sound;
    public float volume = 0.1f;
    public bool isReversed = false;
    Button Button { get { return GetComponent<Button>(); } }
    AudioSource Source { get { return GetComponent<AudioSource>(); } }

    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        Source.clip = sound;
        Source.playOnAwake = false;
        Source.volume = volume;
        if (isReversed) Source.pitch = -1;
        Button.onClick.AddListener(() => PlaySound());
    }

    void PlaySound()
    {
        Source.PlayOneShot(sound);
    }
}
