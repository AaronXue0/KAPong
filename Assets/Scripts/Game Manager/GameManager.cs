using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Role.Playerspace;
using Role.BallSpace;

namespace GameSystem
{
    public class GameManager : MonoBehaviour
    {
        [Header("Lost Point")]
        public GameObject thunder;
        public Vector2 _tStartPos;
        public Vector2 _tEndPos;
        public float _tDuration;
        public float _tSpacing;

        [Header("Score")]
        [SerializeField]
        private int score = 0;
        private float time;

        bool isGameOver = false;

        private float gametime;
        GameEvent gameEvent;
        GameCenterController gameCenter;
        AudioDJ DJ;
        Player player;
        FireBall ball;
        [SerializeField]
        int gameMode = 1;
        bool isFirstGoal = false;
        float ballNum;
        public void Revival()
        {
            gameEvent.RevivalHandling();
            player.GetComponent<Animator>().SetTrigger("revival");
            isGameOver = false;
            player.AbleToMove(true);
        }
        public void Goal(float speed, int state)
        {
            gameEvent.GoalHandling(ref score, speed, state);
            if (score % 10 == 0) ball.SpeedUp = 0.2f;
            if (isFirstGoal == false)
            {
                isFirstGoal = true;
                DJ.SetcionA();
            }
        }
        public void Goal(int addscore)
        {
            gameEvent.GoalAdd(ref score, addscore);
            if (isFirstGoal == false)
            {
                isFirstGoal = true;
                DJ.SetcionA();
            }
        }
        public void LostPoint()
        {
            int n = Random.Range(0, 2);
            Debug.Log(n);
            if (n == 0) StartCoroutine(ModeOne());
            if (n == 1) ModeTwo();
        }
        public IEnumerator ModeOne()
        {
            Vector2 pos = _tStartPos;
            while (pos.x < _tEndPos.x)
            {
                yield return new WaitForSeconds(_tDuration);
                Instantiate(thunder, pos, Quaternion.Euler(0f, 0f, 90f));
                pos = new Vector2(pos.x + _tSpacing, pos.y);
            }
        }
        public void ModeTwo()
        {

            Vector2 pos = _tStartPos * Random.Range(1, 3);
            while (pos.x < _tEndPos.x)
            {
                Instantiate(thunder, pos, Quaternion.Euler(0f, 0f, 90f));
                pos = new Vector2(pos.x + _tSpacing * 1.5f, pos.y);
            }
        }

        public void ShuffleAbility()
        {
            gameEvent.Shuffle(callbackAbilityTrigger);
        }
        void callbackAbilityTrigger(int result)
        {
            ball.AbilityTrigger(result + 1);
        }
        public void Instantiate()
        {
            gameEvent.InstantiateBall();
        }
        public void GameOver()
        {
            isGameOver = true;
            gameEvent.GameOver(score);
            player.AbleToMove(false);
            gameCenter.ReportScoreToLeaderboard(score);
        }
        public Vector2 GetBallSpeed()
        {
            return ball.Movement;
        }
        public void PlayerHurt()
        {
            player.Hurt(1);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J)) ShuffleAbility();
            if (Input.GetKeyDown(KeyCode.H)) LostPoint();
            if (Input.GetKeyDown(KeyCode.I)) Instantiate();
            if (isGameOver) return;
            gametime += Time.deltaTime;
            if (gameMode == 1)
                time += Time.deltaTime;
            if (gametime >= 9.5)
            {
                if (time >= 1.0)
                {
                    if (gametime <= 50) ballNum = gametime / 5;
                    for (int i = 0; i < ballNum; i++)
                    {
                        Instantiate();
                    }
                    Goal(1);
                    time = 0;
                }
            }
        }
        private void Awake()
        {
            gameEvent = GetComponent<GameEvent>();
            DJ = GetComponent<AudioDJ>();
            gameCenter = GetComponent<GameCenterController>();
        }
        private void Start()
        {
            player = FindObjectOfType<Player>();
            ball = FindObjectOfType<FireBall>();
        }
    }
}