using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DesactivacionEstatica : MonoBehaviour
{
    public MainPlayer_Interactions player;
    public GameObject MensajePresionaMouse;
    [HideInInspector] public bool enemigoEstaticoDesactivado = false;
    [SerializeField] private GameObject prefabElectricidad;
    public Animator puerta1Animacion;
    public float tiempoDesactivacion;
    private bool prefabElecticidad = false;

    public GameObject prefabElectricidadPistola;
    public Transform spawnPistola;

    private void OnTriggerStay(Collider other)
    {
        if (player.desactivadorObtenido == true)
        {
            MensajePresionaMouse.SetActive(true);
        }
       
        if (player.desactivadorObtenido == true && other.gameObject.tag == "Player" && Input.GetMouseButtonDown(0) && !prefabElecticidad)
        {
            StartCoroutine("Desactivar");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MensajePresionaMouse.SetActive(false);
    }

    public IEnumerator Desactivar()
    {
            MensajePresionaMouse.SetActive(false);
            prefabElecticidad = true;
            GameObject rayosDesactivados = Instantiate(prefabElectricidad, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("Desactivador");
            enemigoEstaticoDesactivado =true;
            yield return new WaitForSeconds(tiempoDesactivacion);
            Destroy(rayosDesactivados);
            prefabElecticidad = false;
    }
}
