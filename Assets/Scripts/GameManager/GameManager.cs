using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Role.Playerspace;

namespace GameManagerSpace
{
    public class GameManager : MonoBehaviour
    {
        UnityEvent m_scoreEvent = new UnityEvent();
        Model model = new Model();
        View view;

        Player player;
        int selectedSceneID;
        bool isGameStarted = false;

        public void GameRevival()
        {
            view.DORevival(GameStart, player);
        }
        public void GameOver()
        {
            TimeScale(0);
            view.DOGameOver();
        }
        public void GameScene(int n)
        {
            selectedSceneID = n;
            view.DOChangeScene();
        }
        public void ChangeScene()
        {
            TimeScale(1);
            SceneManager.LoadScene(selectedSceneID, LoadSceneMode.Single);
        }
        public void GamePause()
        {
            TimeScale(0);
            view.DOPause();
        }
        public void GameResume()
        {
            view.DOResume(TimeScale);
        }
        void TimeScale() { Time.timeScale = 1; }
        void TimeScale(int n) { Time.timeScale = n; }
        void GameStart()
        {
            if(gameObject.activeSelf == false) gameObject.SetActive(true);
            isGameStarted = true;
            player.AbleToMove(true);
        }
        void ScoreAction()
        {
            model.Score = Time.deltaTime;
            view.SetScoreText(model.Score);
        }
        private void Awake()
        {
            view = GetComponent<View>();
        }
        void Start()
        {
            player = FindObjectOfType<Player>();
            view.SetScoreText(model.Score);
            m_scoreEvent.AddListener(ScoreAction);
            view.SceneOpening(GameStart);
        }
        void Update()
        {
            if (isGameStarted == false) return;
            m_scoreEvent.Invoke();
        }
    }
}