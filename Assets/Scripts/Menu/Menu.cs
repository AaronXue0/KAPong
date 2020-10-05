using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using UnityEngine.Playables;
using DG.Tweening;

namespace Menuspace
{
    public class Menu : MonoBehaviour
    {
        public PlayableAsset[] clips;
        PlayableDirector director;

        public void TransitionAToB()
        {
            director.playableAsset = clips[0];
            director.Play();
        }
        public void TransitionBToA()
        {
            director.playableAsset = clips[1];
            director.Play();
        }
        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
        }
        private void Update()
        {

        }
    }

}