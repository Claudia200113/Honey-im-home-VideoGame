using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPlayer_Interactions : MonoBehaviour
{
    public float tiempoDesactivacion;
    public bool desactivadorObtenido = false;
    public GameObject desactivador;
    public GameObject desactivadorJugador;
    [SerializeField] private GameObject anuncioFaltaLlave; //canvas "missing key"

    public GameObject prefabElectricidadPistola;
    [SerializeField] private DesactivacionGatekeeper scriptEnemigo;

    void Update()
    {
        if (desactivadorObtenido == true && Input.GetMouseButtonDown(0))
        {
            StartCoroutine("RayoPistola");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Desactivador")
        {
            desactivadorObtenido = true;
            desactivador.SetActive(false);
            desactivadorJugador.SetActive(true);
        }
        if (other.gameObject.tag == "Escaleras" && scriptEnemigo.llaveFinalConseguida == true)
        {                
             SceneManager.LoadScene("Victoria");
             Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;
        }
        else if (other.gameObject.tag == "Escaleras" && scriptEnemigo.llaveFinalConseguida == false)
        {
            anuncioFaltaLlave.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Escaleras" && scriptEnemigo.llaveFinalConseguida == false)
        {
            anuncioFaltaLlave.SetActive(false);
        }
    }

    public IEnumerator RayoPistola()
    {
        prefabElectricidadPistola.SetActive(true);
        yield return new WaitForSeconds(.4f);
        prefabElectricidadPistola.SetActive(false);
    }
}
