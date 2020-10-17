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

        public int selectedSceneID;

        Player player;
        bool isGameStarted = false;

        public void GameMenu()
        {
            selectedSceneID = 1;
            gameUIEffect.DOMenu();
        }

        public void GameRestart()
        {
            selectedSceneID = 2;
            gameUIEffect.DOMenu();
        }

        public void ChangeScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(selectedSceneID, LoadSceneMode.Single);
        }

        public void GamePause()
        {
            Time.timeScale = 0;
            gameUIEffect.DOPause();
        }

        public void GameResume()
        {
            gameUIEffect.DOResume(ResetTimeScale);
        }

        void ResetTimeScale() { Time.timeScale = 1; }

        private void Awake()
        {
            gameUIEffect = GetComponent<GameUIEffect>();
        }
        void Start()
        {
            gameUIEffect.SetScoreText(gameEvent.Score);
            m_scoreEvent.AddListener(ScoreAction);
            gameUIEffect.SceneOpening(GameStart);
            player = FindObjectOfType<Player>();
        }
        void GameStart()
        {
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
    }
}