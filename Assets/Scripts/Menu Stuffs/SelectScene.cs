using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectScene : MonoBehaviour
{
    public void LoadB(int sceneANumber)
    {
        SceneManager.LoadScene(sceneANumber);
    }
}
