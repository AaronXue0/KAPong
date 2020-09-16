using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameSystem;

public class Goal : MonoBehaviour
{
    [SerializeField]
    float moveDuration = 1;

    Transform target = null;
    Vector2 movement = Vector2.zero;

    void TowardToMovingDirection()
    {
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        movement = target.position - transform.position;
        transform.DOMove((Vector2)transform.position + movement, moveDuration);
        TowardToMovingDirection();
        if(movement.magnitude < 0.2f) {
            transform.DOPause();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        target = GameObject.Find("Target").transform;
    }


}
