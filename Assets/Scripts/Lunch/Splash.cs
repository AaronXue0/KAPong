using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
