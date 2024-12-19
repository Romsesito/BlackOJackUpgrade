using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    private bool isPaused = false;




    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            Resume();
            else
            Pause();
        }
    }

    // Start is called before the first frame update
    public void Pause()
    {
        isPaused = true;
        Time.timeScale=0f;
        pauseMenuUI.SetActive(true);
        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;
    }

    // Update is called once per frame
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    
    }

        public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    public void Quit()
    {
        Application.Quit();
    }
}
