using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Role.PlayerSpace;
using Role.BallSpace;

public class GameManager : MonoBehaviour
{
    [Header("Game Attritubes")]
    [SerializeField]
    float score1;
    [SerializeField]
    float score2;
    [SerializeField]
    float timeLeft;
    [Header("Classes")]
    Player player;
    FireBall ball;

    public void PlayerHurt()
    {
        player.GetHurt();
    }
    public Vector2 GetBallMovement()
    {
        return ball.GetMovement;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ball = FindObjectOfType<FireBall>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
