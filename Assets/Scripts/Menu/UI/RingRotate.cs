using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotate : MonoBehaviour
{
    float angle = 0;

    void Update()
    {
        angle = (angle + 1) % 360;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
