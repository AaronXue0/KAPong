using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using DG.Tweening;
using TMPro;
using Role.Playerspace;

namespace GameManagerSpace
{
    public class View : MonoBehaviour
    {
        //Camera -> UI
        [Header("UI")]
        public GameObject uiCanvas;
        public Button puaseButton;
        public GameObject pauseCanvas;
        public TextMeshProUGUI scoreText;
        public GameObject resumeImage;
        public GameObject maskPanel;
        public GameObject evaluateCanvas;
        [Header("Timeline")]
        public PlayableAsset toMenuClip;
        [Header("Player")]
        public Transform target;

        Camera cam;
        PlayableDirector director;
        float showDuration = 0.3f;
        float transDuration = 1f;
        float zoomDuration = 1f;

        public void SetScoreText(float score)
        {
            if (score <= 0) return;
            scoreText.text = score.ToString("F2");
        }
        public void SceneOpening(System.Action callback)
        {
            Image mask = maskPanel.GetComponent<Image>();
            Image[] ui = uiCanvas.GetComponentsInChildren<Image>();
            System.Action action = () => ObjectActive(maskPanel, false);
            System.Action action1 = () => LoadGameUI(callback, uiCanvas.GetComponentsInChildren<Image>());
            SetCameraField(1);
            ObjectActive(maskPanel, true);
            SceneOpening(mask, ui, action, action1);
        }
        public void DOPause()
        {
            puaseButton.interactable = false;
            ObjectActive(pauseCanvas, true);
            ShowButtons(pauseCanvas.GetComponentsInChildren<Button>());
        }
        public void DOResume(System.Action callback)
        {
            System.Action action = () => puaseButton.interactable = true;
            ObjectActive(pauseCanvas, false);
            DOLocalScale(resumeImage, new Vector3(10, 10, 0), 0f, null);
            DOLocalMove(resumeImage, target.localPosition, 1.5f, action);
            DOLocalScale(resumeImage, new Vector3(0, 0, 0), 1f, callback);
        }
        //Restart & Menu
        public void DOChangeScene()
        {
            ObjectActive(pauseCanvas, false);
            ObjectActive(evaluateCanvas, false);
            TimelinePlay(toMenuClip);
        }
        public void DOGameOver()
        {
            System.Action action = () => ObjectActive(evaluateCanvas, true);
            StartCoroutine(ShowButtons(0.5f, evaluateCanvas.GetComponentsInChildren<Button>(), action));
        }
        public void DORevival(System.Action callback, Player player)
        {
            ObjectActive(evaluateCanvas, false);
            player.Revival(callback);
        }
        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
        }
        private void Start()
        {
            cam = Camera.main;
            puaseButton.interactable = false;
            scoreText.text = "";
        }

        /// <summary>
        /// Object
        /// </summary>
        void ObjectActive(GameObject obj, bool state) { obj.SetActive(state); }

        /// <summary>
        /// Transform
        /// </summary>
        void DOLocalMove(GameObject obj, Vector2 pos, float duration, System.Action callback)
        {
            obj.transform.DOLocalMove(pos, duration).SetUpdate(true).OnComplete(() => callback());
        }
        void DOLocalScale(GameObject obj, Vector2 scale, float duration, System.Action callback)
        {
            obj.transform.DOScale(scale, duration).SetUpdate(true).OnComplete(() => callback());
        }

        /// <summary>
        /// Color
        /// </summary>
        void AlphaHandling(Image image, float fade, float duration, float delay, System.Action callback)
        { image.DOFade(fade, duration).SetUpdate(true).SetDelay(delay).OnComplete(() => callback()); }
        void AlphaHandling(Image[] image, float fade, float duration) { foreach (Image img in image) { img.DOFade(fade, duration).SetUpdate(true); } }

        /// <summary>
        /// Camera
        /// </summary>
        void SetCameraField(int view) { cam.fieldOfView = view; }
        void ZoomCameraField(int view, float duration, float delay, System.Action callback) { cam.DOFieldOfView(view, duration).SetUpdate(true).SetDelay(delay).OnComplete(() => callback()); }

        /// <summary>
        /// Animation/Timeline
        /// </summary>
        void TimelinePlay(PlayableAsset clip)
        {
            director.playableAsset = clip;
            director.Play();
        }

        /// <summary>
        /// Handling
        /// </summary>
        void SceneOpening(Image mask, Image[] ui, System.Action action, System.Action action1)
        {
            AlphaHandling(mask, 1, 0, 0, null);
            AlphaHandling(ui, 0, 0);
            AlphaHandling(mask, 0, 1, 1, action);
            ZoomCameraField(60, 1f, 1f, action1);
        }
        void LoadGameUI(System.Action callback, Image[] images)
        {
            int index = 0;
            foreach (Image image in images)
            {
                if (index == images.Length - 1) image.DOFade(1f, 1f).OnComplete(() => DOResume(callback));
                else image.DOFade(1f, 1f);
                index++;
            }
        }
        void ShowButtons(Button[] btns)
        {
            int index = 0;
            foreach (var btn in btns)
            {
                StartCoroutine(DelayShowing(index, btn.gameObject));
                index++;
            }
        }
        IEnumerator ShowButtons(float sec, Button[] btns, System.Action callback)
        {
            yield return new WaitForSecondsRealtime(sec);
            callback();
            int index = 0;
            foreach (var btn in btns)
            {
                StartCoroutine(DelayShowing(index, btn.gameObject));
                index++;
            }
        }
        IEnumerator DelayShowing(float seconds, GameObject obj)
        {
            Vector2 pos = obj.transform.localPosition;
            Image image = obj.GetComponent<Image>();
            image.color = new Color32(255, 255, 255, 0);
            obj.transform.localPosition += new Vector3(0, 100, 0);
            yield return new WaitForSecondsRealtime(seconds / 3);
            obj.transform.DOLocalMove(pos, showDuration + 0.2f).SetUpdate(true);
            image.DOFade(1f, showDuration).SetUpdate(true);
        }
    }
}