using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{

    public class Control : MonoBehaviour
    {
        FireBall ball;

        void Start()
        {
            ball = GetComponent<FireBall>();
        }
    }
}