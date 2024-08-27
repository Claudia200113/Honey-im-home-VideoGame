using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool ControlsOpen = false;
    public static bool objectiveOpen = false;
    public GameObject pauseMenuUI;
    public GameObject controlsMenu;
    public GameObject ObjectiveMenu;
    public GameObject miniMenuControles;
    public GameObject miniMenuObjectives;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (ControlsOpen)
            {
                DesactivarControles();
            }
            else
            {
                ActivarControles();
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (objectiveOpen)
            {
                CloseObjectives();
            }
            else
            {
                OpenOjectives();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (ControlsOpen == true)
        {
            DesactivarControles();
        }
        if (objectiveOpen == true)
        {
            DesactivarControles();
        }
    }

     public void LoadMenu()
     {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
     }

    public void QuitGame()
    {
       Application.Quit();
    }

    public void DesactivarControles()
    {
        controlsMenu.SetActive(false);
        ControlsOpen = false;
        Time.timeScale = 1f;
    }

    void ActivarControles()
    {
        controlsMenu.SetActive(true);
        Time.timeScale = .5f;
        ControlsOpen = true;
        miniMenuControles.SetActive(false);
        if (objectiveOpen == true)
        {
            CloseObjectives();
        }
    }

    public void CloseObjectives()
    {
        ObjectiveMenu.SetActive(false);
        Time.timeScale = 1f;
        objectiveOpen = false;
    }

    void OpenOjectives()
    {
        ObjectiveMenu.SetActive(true);
        Time.timeScale = .5f;
        objectiveOpen = true;
        miniMenuObjectives.SetActive(false);
        if (ControlsOpen == true)
        {
            DesactivarControles();
        }
    }
}
