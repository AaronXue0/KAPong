using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TextAnimation : MonoBehaviour
{
    public float duration;
    public void DoAnimation(string text)
    {
        GetComponent<Text>().DOText(text, duration);
    }
}
