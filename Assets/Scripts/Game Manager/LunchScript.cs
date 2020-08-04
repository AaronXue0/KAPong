using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LunchScript : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    Color color;

    Material material;
    bool isDissolving = false;
    bool isDissolvingText = false;
    float fade = 0f;

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LunchAnimation()
    {
        isDissolving = true;
    }
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Start()
    {
        image.color = color;
        Invoke("LunchAnimation", 1f);
    }
    void Update()
    {
        if (isDissolving)
        {
            fade += Time.deltaTime * 0.5f;
            
            if (fade >= 1)
            {
                fade = 1;
                isDissolving = false;
                isDissolvingText = true;
            }

            material.SetFloat("_Fade", fade);
        }
        if (isDissolvingText)
        {
            color.a += Time.deltaTime * 0.5f;
            if (color.a >= 1.5f)
            {
                color.a = 255;
                isDissolvingText = false;
                scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MenuScene, LoadSceneMode.Single));
            }

            image.color = color;
        }
    }
}
