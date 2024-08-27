using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    public void irJuego()
    {
        SceneManager.LoadScene("Juego");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void irMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void irCreditos()
    {
        SceneManager.LoadScene("Credits");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
