using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuCanvas;
    public string MenuScene = "MainMenu";

    // To save the sensitivity
    //PlayerPrefs.SetFloat("MouseSensitivity", newXSensitivity);

    // To load the sensitivity
    //float sensitivity = PlayerPrefs.GetFloat("MouseSensitivity", newYSensitivity);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ManagerGame.showInventory)
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MenuScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    public void SetXSensitivity (float newXSensitivity)
    {
        PlayerMovement.lookSpeedX = newXSensitivity;
    }

    public void SetYSensitivity(float newYSensitivity)
    {
        PlayerMovement.lookSpeedY = newYSensitivity;
    }

}
