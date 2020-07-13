using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerItem", order = 1)]
public class PlayerItem : ScriptableObject
{
    public int money = 100;
}
