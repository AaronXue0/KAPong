using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Puase Effect")]
    public GameObject pauseCanvas;
    public GameObject[] pauseButtons;
    public GameObject musicButton;
    public float delayFadeTime;
    public float buttonFlySpeed;
    public float buttonFadeInSpeed;

    [Header("Scene Transition")]
    public Transform exitPos;
    public Image transitionPanelMask;
    public float cameraTransferSpeed;
    public float cameraZoomSpeed;
    public byte maskingSpeed;
    public byte unmaskingSpeed;

    [Header("Game Control")]
    public GameObject gameMenuButton;

    int selectedScene = 2;
    Camera cam;
    bool isMuted = false;

    public void GameMusic() { DoMute(); }
    public void GameRestart(int id) { SceneEffectHandler(id); }
    public void GameResume() { pauseCanvas.SetActive(false); }
    public void GameMenu(int id) { SceneEffectHandler(id); }
    public void GamePause() { DoPause(); }
    void Start()
    {
        cam = Camera.main;
        EnterGameScene();
    }
    void GameStart()
    {
        gameMenuButton.SetActive(true);
    }
    void EnterGameScene()
    {
        cam.transform.position = new Vector3(exitPos.position.x, exitPos.position.y, -10);
        StartCoroutine(CameraUnmasking());
    }
    void DoMute()
    {
        Image image = musicButton.GetComponent<Image>();
        if (isMuted)
        {
            isMuted = false;
            Color color = Color.white;
            image.color = color;
        }
        else
        {
            isMuted = true;
            Color color = Color.black;
            image.color = color;
        }
    }
    void SceneEffectHandler(int id)
    {
        selectedScene = id;
        transitionPanelMask.enabled = true;
        pauseCanvas.SetActive(false);
        StartCoroutine(CameraMasking());
        StartCoroutine(CameraTransferCoroutine(cam.transform.position, new Vector3(exitPos.position.x, exitPos.position.y, -10), cameraTransferSpeed));
        StartCoroutine(CameraZoom(60, 0));
    }
    void DoPause()
    {
        pauseCanvas.SetActive(true);
        PauseButtonsEffect(0, 1);
    }
    void PauseButtonsEffect(int start, int end)
    {
        for (int i = 0; i < pauseButtons.Length; i++)
        {
            StartCoroutine(ButtonFlyIn(i));
            StartCoroutine(ButtonFadeIn(i, start, end));
        }
    }
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
    IEnumerator ButtonFadeIn(int n, int start, int end)
    {
        Image image = pauseButtons[n].GetComponent<Image>();
        Color color = Color.white;
        color.a = start;
        image.color = color;
        float timer = 0;
        yield return new WaitForSeconds(n * delayFadeTime);
        while (color.a != end)
        {
            timer += Time.deltaTime * buttonFadeInSpeed;
            color.a = Mathf.Lerp(start, end, timer);
            image.color = color;
            yield return null;
        }
    }
    IEnumerator CameraTransferCoroutine(Vector3 aPos, Vector3 bPos, float speed)
    {
        float timer = 0;
        while (cam.transform.position != bPos)
        {
            timer += speed * Time.deltaTime;
            cam.transform.position = Vector3.Lerp(aPos, bPos, timer);
            yield return null;
        }
    }
    IEnumerator CameraZoom(float aField, float bField)
    {
        float timer = 0;
        while (cam.fieldOfView != bField)
        {
            timer += cameraZoomSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Lerp(aField, bField, timer);
            yield return null;
        }
        if (bField >= 60) GameStart();
    }
    IEnumerator CameraMasking()
    {
        byte timer = 175;
        transitionPanelMask.GetComponent<Image>().color = new Color32(0, 0, 0, 175);
        while (timer < 255)
        {
            timer += maskingSpeed;
            transitionPanelMask.GetComponent<Image>().color = new Color32(0, 0, 0, timer);
            yield return null;
        }
        SceneManager.LoadScene(selectedScene);
    }
    IEnumerator CameraUnmasking()
    {
        byte timer = 255;
        transitionPanelMask.GetComponent<Image>().color = new Color32(0, 0, 0, timer);
        while (timer > 175)
        {
            timer -= unmaskingSpeed;
            transitionPanelMask.GetComponent<Image>().color = new Color32(0, 0, 0, timer);
            yield return null;
        }
        transitionPanelMask.enabled = false;
        StartCoroutine(CameraZoom(0f, 60f));
        StartCoroutine(CameraTransferCoroutine(cam.transform.position, new Vector3(0, 1, -10), cameraTransferSpeed));
    }
}
