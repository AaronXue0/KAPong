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
            player.Hurt();
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
            if (Input.GetKeyDown(KeyCode.H)) GameOver();
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