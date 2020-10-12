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
        public PlayableAsset[] unSupportedClips;
        public PlayableAsset supportClip;
        PlayableDirector director;

        public bool appFocus = true;
        bool isOpeningURL;
        Vector2 scale;
        bool supported;

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
        public void SinglePlayer()
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        public void AboutUs(string url)
        {
            Application.OpenURL(url);
        }
        public void PlayThanksFeedback()
        {
            if (supported == false) return;
            director.playableAsset = supportClip;
            director.Play();
        }
        public void TransitionAToB()
        {
            if (supported) director.playableAsset = clips[0];
            else director.playableAsset = unSupportedClips[0];
            director.Play();
        }
        public void TransitionBToA()
        {
            if (supported) director.playableAsset = clips[1];
            else director.playableAsset = unSupportedClips[1];
            director.Play();
        }
        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
        }
        private void Start()
        {
            supported = SystemInfo.supportsComputeShaders && SystemInfo.maxComputeBufferInputsVertex != 0;
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