using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Role.Playerspace;

namespace TimeMode
{
    public class GameManager : MonoBehaviour
    {
        UnityEvent m_scoreEvent = new UnityEvent();
        GameEvent gameEvent = new GameEvent();
        GameUIEffect gameUIEffect;

        Player player;
        int selectedSceneID;
        bool isGameStarted = false;

        public void GameRevival()
        {
            gameUIEffect.DORevival(GameStart, player);
        }
        public void GameOver()
        {
            TimeScale(0);
            gameUIEffect.DOGameOver();
        }
        public void GameScene(int n)
        {
            selectedSceneID = n;
            gameUIEffect.DOChangeScene();
        }
        public void ChangeScene()
        {
            TimeScale(1);
            SceneManager.LoadScene(selectedSceneID, LoadSceneMode.Single);
        }
        public void GamePause()
        {
            TimeScale(0);
            gameUIEffect.DOPause();
        }
        public void GameResume()
        {
            gameUIEffect.DOResume(TimeScale);
        }
        void TimeScale() { Time.timeScale = 1; }
        void TimeScale(int n) { Time.timeScale = n; }
        void GameStart()
        {
            TimeScale();
            isGameStarted = true;
            player.AbleToMove(true);
        }
        void Update()
        {
            if (isGameStarted == false) return;
            m_scoreEvent.Invoke();
        }
        void ScoreAction()
        {
            gameEvent.Score = Time.deltaTime;
            gameUIEffect.SetScoreText(gameEvent.Score);
        }
        private void Awake()
        {
            gameUIEffect = GetComponent<GameUIEffect>();
        }
        void Start()
        {
            player = FindObjectOfType<Player>();
            gameUIEffect.SetScoreText(gameEvent.Score);
            m_scoreEvent.AddListener(ScoreAction);
            gameUIEffect.SceneOpening(GameStart);
        }
    }
}