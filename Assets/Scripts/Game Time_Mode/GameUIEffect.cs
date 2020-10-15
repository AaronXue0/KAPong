using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace TimeMode
{
    public class GameUIEffect : MonoBehaviour
    {
        //Camera -> UI

        public Button puaseButton;
        public GameObject pauseCanvas;
        public TextMeshProUGUI scoreText;
        public GameObject resumeImage;
        public GameObject maskPanel;

        Camera cam;
        float showDuration = 0.5f;
        float transDuration = 1f;
        float zoomDuration = 1f;

        public void SceneOpening()
        {
            cam.fieldOfView = 0;
            cam.DOFieldOfView(60, 1f);
        }

        public void SetScoreText(float score)
        {
            scoreText.text = score.ToString("F2");
        }

        public void DOMenu(System.Action<int> callback)
        {
            Transform target = GameObject.FindGameObjectWithTag("Player").transform;
            maskPanel.SetActive(true);
            pauseCanvas.SetActive(false);
            maskPanel.GetComponent<Image>().DOFade(1, 0.1f);
            cam.transform.DOMove(target.position, transDuration).SetUpdate(true);
            cam.DOFieldOfView(0, zoomDuration).SetUpdate(true).OnComplete(() => callback(1));
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

        private void Start()
        {
            cam = Camera.main;
        }
    }
}