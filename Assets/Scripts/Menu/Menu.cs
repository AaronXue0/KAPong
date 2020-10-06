// using UnityEngine.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using DG.Tweening;

namespace Menuspace
{
    public class Menu : MonoBehaviour
    {
        public GameObject pausePanel;
        public GameObject spaceship;
        public PlayableAsset[] clips;
        public PlayableAsset supportClip;
        PlayableDirector director;

        public bool appFocus;
        bool isOpeningURL;
        Vector2 scale;

        void OnGUI()
        {
            if (appFocus == false)
            {
                pausePanel.SetActive(true);
            }
        }

        void OnApplicationFocus(bool focusStatus)
        {
            appFocus = focusStatus;
        }
        public void AboutUs(GameObject btn)
        {
            Application.OpenURL("https://www.facebook.com/Game-Starry-109356847225732/?view_public_for=109356847225732");
        }
        public void PlayThanksFeedback()
        {
            director.playableAsset = supportClip;
            director.Play();
        }
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
        void Update()
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                if (pausePanel.activeSelf) pausePanel.SetActive(false);
            }
        }
    }

}