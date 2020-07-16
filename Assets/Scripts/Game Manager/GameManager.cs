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
    GameEvent gameEvent;
    Player player;
    FireBall ball;

    public void FireBallAbilityTrigger(int choosen)
    {
        switch(choosen){
            case 0:
                ball.sinWave = true;
                break;
            case 1:
                ball.transparency = true;
                break;
            case 2:
                ball.sinWave = true;
                break;
            case 3:
                ball.sinWave = true;
                break;
            default:
                Debug.Log(choosen);
                break;
        }
    }
    public void ShuffleFireBallAbility()
    {
        gameEvent.Shuffle();
    }
    public void PlayerHurt()
    {
        health1 -= 40;
        player.GetHurt(health1);
        SetPlayerSlider(playerSlider.normalizedValue);
    }
    public Vector2 GetBallMovement()
    {
        return ball.GetMovement;
    }
    void Awake()
    {
        gameEvent = GetComponent<GameEvent>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        ball = FindObjectOfType<FireBall>();

        //Set Player Slider
        playerSlider.value = health1;
        playerSlider.maxValue = health1;
        SetPlayerSlider(1f);
    }
    void SetPlayerSlider(float value)
    {
        playerSlider.value = health1;
        fill.color = gradient.Evaluate(value);
    }
}
