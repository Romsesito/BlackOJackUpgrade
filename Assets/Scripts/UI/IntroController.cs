using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public float introDuration = 3f; 
    public string nextSceneName = "MainMenu"; 

    void Start()
    {
       
        Invoke("LoadNextScene", introDuration);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
