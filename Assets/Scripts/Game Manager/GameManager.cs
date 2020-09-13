using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Role.Playerspace;
using Role.BallSpace;

public class GameManager : MonoBehaviour
{
    Player player;
    FireBall ball;

    public void GameOver()
    {

    }   

    public Vector2 GetBallSpeed()
    {
        return ball.Movement;
    }

    public void PlayerHurt()
    {
        player.Hurt();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        ball = FindObjectOfType<FireBall>();
    }
}