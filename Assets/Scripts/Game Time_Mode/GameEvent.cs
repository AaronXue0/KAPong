using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeMode
{
    public class GameEvent
    {
        float score = 0;
        public float Score { set { score += value; } get { return score; } }
    }
}