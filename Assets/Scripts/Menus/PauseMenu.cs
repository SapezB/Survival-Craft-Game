using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public ThirdPersonController thirdPersonController;
    public GameObject pauseMenuUI;
    void Start()
    {
        pauseMenuUI.SetActive(false); // This will ensure the pause menu is hidden on start.
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            if (GameIsPaused)
            {
                Debug.Log("Resuming game");
                Resume();

            } else
            {
                Debug.Log("Pausing game");
                Pause();
            }
        }
    }

   public void Resume()
    {
        Debug.Log("Resume called");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }
    void Pause()
    {
        Debug.Log("Pause called");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        
    }
    public void LoadMenu()

    {
        Time.timeScale = 1f;    
        SceneManager.LoadScene("Main Menu");
            
                
            }

    public void QuitGame()
    {
        Application.Quit();

    }
}
