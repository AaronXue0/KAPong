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
        Model model = Model.Instance;
        View view;
        GameObjectPool pool;

        Player player;
        int selectedSceneID;
        bool isGameStarted = false;
        float time = 0;
        float timeDuration = 0.5f;

        public void InstantiateBall()
        {
            // Instantiate(model.Ball, model.InstantiatePlace(1), new Quaternion());
        }
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
            if (gameObject.activeSelf == false) gameObject.SetActive(true);
            TimeScale();
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
            pool = GetComponent<GameObjectPool>();
        }
        void Start()
        {
            player = FindObjectOfType<Player>();
            m_scoreEvent.AddListener(ScoreAction);
            view.SceneOpening(GameStart);
        }

        void Update()
        {
            if (isGameStarted == false) return;
            m_scoreEvent.Invoke();
            if (Time.time > time + timeDuration)
            {
                time = Time.time;
                pool.ReUse(model.InstantiatePlace(1), new Quaternion());
            }
            if (timeDuration > 0.1f) timeDuration -= Time.deltaTime * 0.5f;
        }
    }
}