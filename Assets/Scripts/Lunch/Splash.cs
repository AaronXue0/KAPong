using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    void Awake()
    {
    }
    void Start()
    {
        SceneManager.LoadScene(1);
    }
}
