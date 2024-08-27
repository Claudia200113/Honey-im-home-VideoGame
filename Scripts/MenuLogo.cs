using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuLogo : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
                SceneManager.LoadScene("MenuPrincipal");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
        }
    }
}
