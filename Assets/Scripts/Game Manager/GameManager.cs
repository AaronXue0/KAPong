using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Role.PlayerSpace;
using Role.BallSpace;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Slider playerSlider;
    public Gradient gradient;
    public Image fill;
    [Header("Game Attritubes")]
    [SerializeField]
    float score1;
    [SerializeField]
    float health1;
    [SerializeField]
    float score2;
    [SerializeField]
    float health2;
    [SerializeField]
    float timeLeft;
    [Header("Classes")]
    Player player;
    FireBall ball;

    public void PlayerHurt()
    {
        health1 -= 40;
        player.GetHurt(health1);
        playerSlider.value = health1;
        fill.color = gradient.Evaluate(playerSlider.normalizedValue);
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

        playerSlider.value = health1;
        playerSlider.maxValue = health1;
        fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
