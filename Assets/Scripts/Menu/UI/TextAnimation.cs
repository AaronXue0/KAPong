using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TextAnimation : MonoBehaviour
{
    public float duration;
    bool isAnimating = false;
    public void DoAnimation(string text)
    {
        if (isAnimating == true) return;
        isAnimating = true;
        GetComponent<Text>().text = "";
        GetComponent<Text>().DOText(text, duration).OnComplete(() => isAnimating = false);
    }
}
