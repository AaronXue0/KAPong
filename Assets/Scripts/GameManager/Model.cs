using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagerSpace
{
    public class Model
    {
        float score = 0;
        public float Score { set { score += value; } get { return score; } }
    }
}