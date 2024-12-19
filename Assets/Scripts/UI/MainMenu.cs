using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        Debug.Log("MainMenu started and set active.");
    }

    // Method called when the play button is clicked
    public void PlayButtonClicked()
    {
        Debug.Log("PlayButtonClicked called");
        Time.timeScale = 1f;
        Debug.Log("Time.timeScale set to 1f");
        
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync("MainMenu");
        if (unloadOperation != null)
        {
            Debug.Log("MainMenu scene unload started");
        }
        else
        {
            Debug.LogError("Failed to start unloading MainMenu scene");
        }

        SceneManager.LoadScene("JUEGO");
        Debug.Log("JUEGO scene load started");
    }

    // Method called when the quit button is clicked
    public void QuitButtonClicked()
    {
        Debug.Log("QuitButtonClicked called");
        Application.Quit();
    }
}