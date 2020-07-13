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
    int[] scores;
    [SerializeField]
    float health1;
    [SerializeField]
    float health2;
    [SerializeField]
    float timeLeft;
    [SerializeField]
    Vector2[] spawnPoint;
    [SerializeField]
    GameObject playerObject;
    [Header("Score Stuffs Component")]
    [SerializeField]
    GameObject[] flags;
    [SerializeField]
    Text[] scoreText;
    [SerializeField]
    GameObject[] scoreEffect;
    int selectScore;
    [Header("Ball Instantiate")]
    [SerializeField]
    GameObject fireball;
    [SerializeField]
    Vector2[] ballSpawnPoint;
    [Header("Classes")]
    GameEvent gameEvent;
    Player player;
    FireBall ball;

    public void ScoreAdd(int value)
    {
        scores[selectScore] += value;
        scoreText[selectScore].text = scores[selectScore].ToString(); 
        BallGenerate();
    }
    public void Goal(GameObject ball, GameObject flag)
    {
        if (flag.name == flags[0].name)
        {
            selectScore = 1;
            Instantiate(scoreEffect[0], ball.transform.position, ball.transform.rotation);
        }
        else if (flag.name == flags[1].name)
        {
            selectScore = 0;
            Instantiate(scoreEffect[1], ball.transform.position, ball.transform.rotation);
        }
        if(health1 < 0)
        {
            health1 = 100;
            Spawn();
        }
    }
    public void Spawn()
    {
        GameObject obj = GameObject.FindWithTag("Player");
        Destroy(obj);
        Instantiate(playerObject, spawnPoint[0], new Quaternion(0,0,0,0));
        player = FindObjectOfType<Player>();
    }
    public void FireBallAbilityTrigger(int choosen)
    {
        switch (choosen)
        {
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
    public void SetPlayerSlider(float value)
    {
        playerSlider.value = health1;
        fill.color = gradient.Evaluate(value);
    }
    public Transform GetBallTransform()
    {
        if(ball == null) return null;
        return ball.transform;
    }
    public Vector2 GetBallMovement()
    {
        if(ball == null) return Vector2.zero;
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
    void BallGenerate()
    {
        if(selectScore == 0){
            Instantiate(fireball, ballSpawnPoint[0], Quaternion.Euler(new Vector3(0, 0, 0)));
        }else if(selectScore == 1){
            Instantiate(fireball, ballSpawnPoint[1], Quaternion.Euler(new Vector3(0, 0, 180)));
        }
        ball = FindObjectOfType<FireBall>();
    }
}
