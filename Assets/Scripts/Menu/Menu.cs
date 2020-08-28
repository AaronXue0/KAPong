using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField]
    Text broadCastText;

    public void LoadScene(int sceneANumber)
    {
        SceneManager.LoadScene(sceneANumber);
    }
    public void OpenLinking(string url)
    {
        Application.OpenURL(url);
    }
    public void BroadCast(string news)
    {
        broadCastText.text = news;
    }
    public void ColoredText(float a)
    {
        Color color = broadCastText.color;
        color.a = a;
        broadCastText.color = color;
    }  
}
