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

        GameEvent gameEvent;
        AudioDJ DJ;
        Player player;
        FireBall ball;

        bool isFirstGoal = false;

        public void Goal(float speed, int state)
        {
            gameEvent.GoalHandling(ref score, speed, state);
            if (isFirstGoal == false)
            {
                isFirstGoal = true;
                DJ.SetcionA();
            }
        }
        public void LostPoint()
        {
            int n = Random.Range(0,2);
            Debug.Log(n);
            if(n == 0) StartCoroutine(ModeOne());
            if(n == 1) ModeTwo();
        }
        public IEnumerator ModeOne()
        {
            Vector2 pos = _tStartPos;
            while (pos.x < _tEndPos.x)
            {
                yield return new WaitForSeconds(_tDuration);
                Instantiate(thunder, pos, Quaternion.Euler (0f, 0f, 90f));
                pos = new Vector2(pos.x + _tSpacing, pos.y);
            }
        }
        public void ModeTwo()
        {

            Vector2 pos = _tStartPos * Random.Range(1,3);
            while (pos.x < _tEndPos.x)
            {
                Instantiate(thunder, pos, Quaternion.Euler (0f, 0f, 90f));
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
        public void GameOver()
        {
            gameEvent.GameOver(score);
            player.AbleToMove(false);
        }
        public Vector2 GetBallSpeed()
        {
            return ball.Movement;
        }
        public void PlayerHurt()
        {
            player.Hurt();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J)) ShuffleAbility();
            if (Input.GetKeyDown(KeyCode.H)) LostPoint();
        }
        private void Awake()
        {
            gameEvent = GetComponent<GameEvent>();
            DJ = GetComponent<AudioDJ>();
        }
        private void Start()
        {
            player = FindObjectOfType<Player>();
            ball = FindObjectOfType<FireBall>();
        }
    }
}