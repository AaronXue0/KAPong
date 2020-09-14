using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Role.Playerspace;

namespace GameSystem
{
    public class GameUIEffectManager : MonoBehaviour
    {
        [Header("Scene Enterance")]
        public float camFOVDuration;
        public float camMoveDuration;
        public Image maskPannel;
        public float maskFadeDuration;
        [Header("Pause Menu Effect")]
        public float fadeDuration;
        public float flyDuration;
        //Old System
        [Header("Puase Effect")]
        public Transform exitPos;
        public GameObject pauseCanvas;
        public GameObject[] pauseButtons;
        public GameObject musicButton;
        public float delayOrderTime;

        [Header("Game Control")]
        public GameObject playerControlCanvas;
        public GameObject gameUI;
        public float dropDuration;
        public Text counterText;

        Camera cam;
        bool isMuted = false;

        public void GameMusic() { DoMute(); }
        public void GameRestart(int id) { SceneEffectHandler(id); }
        public void GameResume() { pauseCanvas.SetActive(false); }
        public void GameMenu(int id) { SceneEffectHandler(id); }
        public void GamePause() { DoPause(); }
        public void LoadScene(int id) { SceneManager.LoadScene(id); }

        Player player;

        public Color whiteT
        {
            get
            {
                Color color = Color.white;
                color.a = 0;
                return color;
            }
        }
        public Color blackT
        {
            get
            {
                Color color = Color.black;
                color.a = 0;
                return color;
            }
        }

        void Start()
        {
            cam = Camera.main;
            cam.fieldOfView = 0;
            player = FindObjectOfType<Player>();
            Invoke("EnterGameScene", 0.3f);
        }
        void GameViewStart()
        {
            maskPannel.enabled = false;
            LoadGameUI();
        }
        void GameControllerStart()
        {
            player.AbleToMove(true);
        }
        void LoadGameUI()
        {
            Image[] objects = gameUI.GetComponentsInChildren<Image>();
            gameUI.SetActive(true);
            foreach (Image image in objects)
            {
                image.color = whiteT;
                image.DOFade(1, 1);
                Vector3 posB = image.transform.localPosition;
                image.transform.localPosition += new Vector3(0, 100, 0);
                image.transform.DOLocalMove(posB, dropDuration).OnComplete(() => StartCoroutine(CountDownStart()));
                image.transform.DOShakeRotation(dropDuration * 2, new Vector3(0, 0, 20), 5, 180, false);
            }
            playerControlCanvas.SetActive(true);
        }
        void EnterGameScene()
        {
            cam.transform.position = new Vector3(exitPos.position.x, exitPos.position.y, -10);
            cam.transform.DOMove(new Vector3(0, 0, -10), camMoveDuration).OnComplete(() => GameViewStart());
            cam.DOFieldOfView(60, camFOVDuration);
            maskPannel.DOFade(0f, maskFadeDuration);
        }
        IEnumerator CountDownStart()
        {
            counterText.enabled = true;
            int timer = 3;
            Vector3 posB = counterText.gameObject.transform.localPosition;
            while (timer >= 0)
            {
                yield return new WaitForSeconds(1);
                Color color = blackT;
                counterText.color = color;
                counterText.text = timer.ToString();
                counterText.gameObject.transform.localPosition += new Vector3(0, 100, 0);
                counterText.DOFade(1, 1);
                counterText.gameObject.transform.DOLocalMove(posB, 0.8f);
                timer--;
            }
            counterText.enabled = false;
            GameControllerStart();
        }
        void DoMute()
        {
            Image image = musicButton.GetComponent<Image>();
            if (isMuted)
            {
                isMuted = false;
                Color color = Color.white;
                image.color = color;
                // Music off api.
            }
            else
            {
                isMuted = true;
                Color color = Color.black;
                image.color = color;
                // Music on api.
            }
        }
        void SceneEffectHandler(int id)
        {
            gameUI.SetActive(false);
            maskPannel.enabled = true;
            cam.DOFieldOfView(0, camFOVDuration);
            StartCoroutine(CameraTransfer(cam.transform.position, new Vector3(exitPos.position.x, exitPos.position.y, -10), 1f));
            maskPannel.DOFade(1f, maskFadeDuration).OnComplete(() => LoadScene(id));
        }
        void DoPause()
        {
            pauseCanvas.SetActive(true);
            for (int i = 0; i < pauseButtons.Length; i++)
            {
                TransparencyImage(pauseButtons[i].GetComponent<Image>());
                StartCoroutine(PauseEffectCoroutine(i));
            }
        }
        void TransparencyImage(Image image)
        {
            Color color = whiteT;
            image.color = color;
        }
        IEnumerator PauseEffectCoroutine(int n)
        {
            yield return new WaitForSeconds(n * delayOrderTime);
            Image image = pauseButtons[n].GetComponent<Image>();
            Vector3 to = pauseButtons[n].transform.localPosition;
            pauseButtons[n].transform.localPosition -= new Vector3(300, 0, 0);
            pauseButtons[n].transform.DOLocalMove(to, flyDuration);
            image.DOFade(1, fadeDuration);
        }
        IEnumerator CameraTransfer(Vector3 aPos, Vector3 bPos, float speed)
        {
            float timer = 0;
            while (cam.transform.position != bPos)
            {
                timer += speed * Time.deltaTime;
                cam.transform.position = Vector3.Lerp(aPos, bPos, timer);
                yield return null;
            }
        }
    }
}