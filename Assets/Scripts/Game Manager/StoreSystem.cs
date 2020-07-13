using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour
{
    public GameObject loading;
    public Image[] loadingImages;
    public Sprite[] loadingSprites;
    public GameObject result;
    public Text resultText;
    public Text moneyText;
    public PlayerItem item;

    public void AddMoney(int cost)
    {
        bool isSuccessful = (item.money + cost < 0) ? false : true;
        loading.SetActive(true);
        StartCoroutine(LoadingEffect(0.3f, 0.1f, isSuccessful));
        if (isSuccessful == false) return;
        item.money += cost;
        ChangeMoneyView();
        // DoStore();
    }
    void Start()
    {
        moneyText.text = "Money: " + item.money.ToString();
    }
    void ChangeMoneyView()
    {
        moneyText.text = "Money: " + item.money.ToString();
    }
    IEnumerator LoadingEffect(float duration, float delay, bool isSuccessful)
    {
        int n = 0;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            SetImage(n);
            n = (n + 1) % loadingImages.Length;
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(delay);
        }
        if(isSuccessful) resultText.text = "Success";
        else resultText.text = "Fail";
        loading.SetActive(false);
        result.SetActive(true);
        yield return new WaitForSeconds(1);
        result.SetActive(false);
    }
    void SetImage(int n)
    {
        for (int i = 0; i < loadingImages.Length; i++)
        {
            if (i == n) loadingImages[i].sprite = loadingSprites[1];
            else loadingImages[i].sprite = loadingSprites[0];
        }
    }
}
