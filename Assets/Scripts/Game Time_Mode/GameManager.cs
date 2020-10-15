using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TimeMode
{
    public class GameManager : MonoBehaviour
    {
        UnityEvent m_scoreEvent = new UnityEvent();
        GameEvent gameEvent;
        GameUIEffect gameUIEffect;

        bool isGameStarted = false;

        public void GameMenu()
        {
            gameUIEffect.DOMenu(ChangeScene);
        }

        public void ChangeScene(int id) { SceneManager.LoadScene(id); }

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
            gameEvent = GetComponent<GameEvent>();
        }
        void Start()
        {
            gameUIEffect.SetScoreText(gameEvent.Score);
            m_scoreEvent.AddListener(ScoreAction);
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