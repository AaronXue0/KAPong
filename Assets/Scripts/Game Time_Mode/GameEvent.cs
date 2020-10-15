using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeMode
{
    public class GameEvent : MonoBehaviour
    {
        float score = 0;
        public float Score { set { score += value; } get { return score; } }
    }
}