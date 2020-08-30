using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Puase Effect")]
    public GameObject pauseCanvas;
    public GameObject[] pauseButtons;
    public float delayFadeTime;
    public float buttonFlySpeed;
    public float buttonFadeSpeed;

    bool isAnimated = false;

    public void GameMusic()
    {

    }
    public void GameResume()
    {

    }
    public void GameMenu()
    {

    }
    public void GamePause()
    {
        pauseCanvas.SetActive(true);
        if (isAnimated == false) PauseButtonsEffect();
        isAnimated = true;
    }

    void PauseButtonsEffect()
    {
        for (int i = 0; i < pauseButtons.Length; i++)
        {
            StartCoroutine(ButtonFlyIn(i));
            StartCoroutine(ButtonFadeIn(i));
        }
    }
    //Transparency , transform
    IEnumerator ButtonFlyIn(int n)
    {
        Vector3 posB = pauseButtons[n].transform.localPosition;
        pauseButtons[n].transform.localPosition -= new Vector3(300, 0, 0);
        Vector3 posA = pauseButtons[n].transform.localPosition;
        float timer = 0;
        yield return new WaitForSeconds(n * delayFadeTime);
        while (pauseButtons[n].transform.localPosition != posB)
        {
            timer += Time.deltaTime * buttonFlySpeed;
            pauseButtons[n].transform.localPosition = Vector2.Lerp(posA, posB, timer);
            yield return null;
        }
    }
    IEnumerator ButtonFadeIn(int n)
    {
        Image image = pauseButtons[n].GetComponent<Image>();
        Color color = Color.white;
        color.a = 0;
        image.color = color;
        float timer = 0;
        yield return new WaitForSeconds(n * delayFadeTime);
        while (color.a < 1)
        {
            timer += Time.deltaTime * buttonFadeSpeed;
            color.a = Mathf.Lerp(0, 1, timer);
            image.color = color;
            yield return null;
        }
    }
}
