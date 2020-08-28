using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LunchScript : MonoBehaviour
{
    [SerializeField]
    Image textImage;
    [SerializeField]
    Image textImageBackground;
    [SerializeField]
    Color color;
    [SerializeField]
    Transform targetPos;
    Material material;
    bool isDissolving = false;
    bool isDissolvingText = false;
    float fade;
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
        textImage.color = color;
        textImageBackground.color = color;
        Invoke("LunchAnimation", 1f);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos.position, Time.deltaTime * 2.3f);
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
            if(transform.localScale.x > 0) transform.localScale -= new Vector3(Time.deltaTime*0.5f,Time.deltaTime*0.5f,Time.deltaTime);
            color.a += Time.deltaTime * 0.5f;
            if (color.a >= 1.5f)
            {
                color.a = 255;
                isDissolvingText = false;
                SceneManager.LoadScene(1);
                scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MenuScene, LoadSceneMode.Single));
            }
            fade -= Time.deltaTime * 0.5f;
            if (fade <= 0)
            {
                fade = 0;
            }
            material.SetFloat("_Fade", fade);
            textImage.color = color;
            textImage.color = color;
        }
    }
}
