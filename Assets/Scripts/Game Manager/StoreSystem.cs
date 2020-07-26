using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSystem : MonoBehaviour
{
    public RectTransform content;
    public Text moneyText;
    public PlayerItem item;

    public void AddMoney()
    {
        item.money += 10;
        ChangeMoneyView();
    }
    public void AddMoney(int cost)
    {
        if (item.money + cost < 0)
        {
            StartCoroutine(ShakeDelay(0.15f, 0.4f));
            return;
        }
        item.money += cost;
        ChangeMoneyView();
    }
    void Start()
    {
        moneyText.text = "Money: " + item.money.ToString();
    }
    void ChangeMoneyView()
    {
        moneyText.text = "Money: " + item.money.ToString();
    }
    IEnumerator ShakeDelay(float duration, float magnitude)
    {
        Vector3 originalPos = content.localPosition;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            content.localPosition = new Vector3(x,y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        content.localPosition = originalPos;
    }
}
