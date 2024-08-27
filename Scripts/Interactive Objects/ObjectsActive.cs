using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsActive : MonoBehaviour
{
    public GameObject NotaEscrita;
    public GameObject ObjectVisual;
    public GameObject CamVisual;
    public GameObject ObjectScene;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject objectivesMenu;

    public bool Active;
    public bool carta1activa = false;
    public GameObject desactivador;
    public GameObject TextoPressF;

    private void Update()
    {
        if(Input.GetKey(KeyCode.F)&& Active == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
            NotaEscrita.SetActive(true);
            CamVisual.SetActive(true);
            ObjectVisual.SetActive(true);
            ObjectScene.SetActive(false);
            Time.timeScale = 0f;
            TextoPressF.SetActive(false);
            carta1activa = true;
            desactivador.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Bitacora");
            controlsMenu.SetActive(false);
            objectivesMenu.SetActive(false);

        }

        if(Input.GetKey(KeyCode.Tab)&& Active == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            NotaEscrita.SetActive(false);
            CamVisual.SetActive(false);
            ObjectVisual.SetActive(false);
            ObjectScene.SetActive(true);
            Time.timeScale = 1f;
            controlsMenu.SetActive(true);
            objectivesMenu.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Active = true;
            TextoPressF.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Active = false;
            NotaEscrita.SetActive(false);
            CamVisual.SetActive(false);
            ObjectVisual.SetActive(false);
            ObjectScene.SetActive(true);
            TextoPressF.SetActive(false);

        }
    }
}
