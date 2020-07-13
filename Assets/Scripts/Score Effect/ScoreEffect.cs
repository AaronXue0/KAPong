using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEffect : MonoBehaviour
{
    [SerializeField]
    string objName;
    [SerializeField]
    int score;
    [SerializeField]
    float speed;
    [SerializeField]
    float distance;

    GameManager gm;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        target = GameObject.Find(objName);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToScoreText();
    }
    void MoveToScoreText()
    {
        Vector2 movement = target.transform.position - transform.position;
        if(movement.x < distance && movement.y < distance) DoGoal();
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    void DoGoal()
    {
        gm.ScoreAdd(score);
        Destroy(this.gameObject);
    }
}
