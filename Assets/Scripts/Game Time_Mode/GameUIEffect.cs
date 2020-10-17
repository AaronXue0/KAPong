using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using DG.Tweening;
using TMPro;

namespace TimeMode
{
    public class GameUIEffect : MonoBehaviour
    {
        //Camera -> UI
        [Header("UI")]
        public GameObject uiCanvas;
        public Button puaseButton;
        public GameObject pauseCanvas;
        public TextMeshProUGUI scoreText;
        public GameObject resumeImage;
        public GameObject maskPanel;
        [Header("Timeline")]
        public PlayableAsset toMenuClip;

        Camera cam;
        PlayableDirector director;
        float showDuration = 0.5f;
        float transDuration = 1f;
        float zoomDuration = 1f;

        public void SceneOpening(System.Action callback)
        {
            maskPanel.SetActive(true);
            OpacityImage(maskPanel.GetComponent<Image>());
            TransparencyImages(uiCanvas.GetComponentsInChildren<Image>());
            cam.fieldOfView = 1;
            cam.DOFieldOfView(60, 1f).SetDelay(1f).OnComplete(() => LoadGameUI(callback));
            maskPanel.GetComponent<Image>().DOFade(0f, 1f).SetDelay(1f).OnComplete(() => maskPanel.SetActive(false));
        }
        void TransparencyImages(Image[] img)
        {
            foreach (Image image in img)
            {
                image.DOFade(0, 0);
            }
        }
        void OpacityImage(Image img)
        {
            img.DOFade(1, 0);
        }
        void LoadGameUI(System.Action callback)
        {
            Image[] images = uiCanvas.GetComponentsInChildren<Image>();
            int index = 0;
            foreach (Image image in images)
            {
                if (index == images.Length - 1) image.DOFade(1f, 1f).OnComplete(() => DOResume(callback));
                else image.DOFade(1f, 1f);
                index++;
            }
        }

        public void SetScoreText(float score)
        {
            scoreText.text = score.ToString("F2");
        }

        public void DOMenu()
        {
            director.playableAsset = toMenuClip;
            director.Play();
            pauseCanvas.SetActive(false);
        }

        public void DOPause()
        {
            puaseButton.interactable = false;
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
            ShowPauseButtons();
        }

        void ShowPauseButtons()
        {
            int index = 0;
            Button[] btns = pauseCanvas.GetComponentsInChildren<Button>();
            foreach (var btn in btns)
            {
                StartCoroutine(ShowDelay(index, btn.gameObject));
                index++;
            }
        }

        IEnumerator ShowDelay(float seconds, GameObject obj)
        {
            Vector2 pos = obj.transform.localPosition;
            Image image = obj.GetComponent<Image>();
            image.color = new Color32(255, 255, 255, 0);
            obj.transform.localPosition += new Vector3(0, 100, 0);
            yield return new WaitForSecondsRealtime(seconds / 3);
            obj.transform.DOLocalMove(pos, showDuration + 0.2f).SetUpdate(true);
            image.DOFade(1f, showDuration).SetUpdate(true);
        }

        public void DOResume(System.Action callback)
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").transform;
            resumeImage.transform.localScale = new Vector3(10, 10, 0);
            pauseCanvas.SetActive(false);
            resumeImage.transform.DOMove(target.position, 1f).OnComplete(() => puaseButton.interactable = true);
            resumeImage.transform.DOScale(new Vector3(0, 0, 0), 2f).SetUpdate(true).OnComplete(() => callback());
        }

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
        }

        private void Start()
        {
            cam = Camera.main;
        }
    }
}