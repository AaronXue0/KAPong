using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEvent : MonoBehaviour
{
    [Header("Fire Ball Ability Events")]
    [SerializeField]
    Image abilityBox;
    [SerializeField]
    Sprite[] abilitySprite;
    [SerializeField]
    float shuffleDuration;
    [SerializeField]
    int maxShuffleTime;
    int shuffleTime = 0;

    int randomAbility;
    GameManager gm;

    public void Shuffle()
    {
        shuffleTime = maxShuffleTime;
        StartCoroutine(SetImage(shuffleDuration, 0));
    }
    void Awake()
    {  
        gm = GetComponent<GameManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Shuffle();
    }
    IEnumerator SetImage(float delay, int n)
    {
        yield return new WaitForSeconds(delay);
        if (n < abilitySprite.Length)
        {
            abilityBox.sprite = abilitySprite[n];
            StartCoroutine(SetImage(shuffleDuration, n + 1));
        }
        else if (shuffleTime > 0)
        {
            shuffleTime--;
            StartCoroutine(SetImage(shuffleDuration, 0));
        }
        else
        {
            randomAbility = Random.Range(0, abilitySprite.Length);
            abilityBox.sprite = abilitySprite[randomAbility];
            gm.FireBallAbilityTrigger(randomAbility);
        }
    }
}
