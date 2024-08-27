using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DesactivacionGatekeeper : MonoBehaviour
{
    public MainPlayer_Interactions player;
    [SerializeField] public NavMeshAgent navmeshEnemigo;
    public float tiempoDesactivacion;
    public EnemyMovement Movement;
    public GameObject MensajePresionaMouse;
    [SerializeField] private GameObject prefabElectricidad;
    public GameObject prefabLlave;
    private bool prefabElecticidad = false;
    public bool llaveFinalConseguida = false;
    [SerializeField] private Animator animatorGato;
    [SerializeField] private GameObject LlaveCargada;

    private void OnTriggerStay(Collider other)
    {
        if (player.desactivadorObtenido == true)
        {
            MensajePresionaMouse.SetActive(true);
        }

        if (player.desactivadorObtenido == true && other.gameObject.tag == "Player" && Input.GetMouseButtonDown(0) && !prefabElecticidad)
        {
            MensajePresionaMouse.SetActive(false);
            StartCoroutine("Desactivar");
            FindObjectOfType<AudioManager>().Play("Desactivador");

            if (!llaveFinalConseguida)
            {
                LlaveCargada.SetActive(false);
                llaveFinalConseguida = true;
                FindObjectOfType<AudioManager>().Play("Llave");
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        MensajePresionaMouse.SetActive(false);
    }

    public IEnumerator Desactivar()
    {
        Movement.enabled = false;
        navmeshEnemigo.enabled = false;
        prefabElecticidad = true;
        animatorGato.enabled = false;
        GameObject rayosDesactivados = Instantiate(prefabElectricidad, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(tiempoDesactivacion);
        Destroy(rayosDesactivados);
        Movement.enabled = true;
        navmeshEnemigo.enabled = true;
        prefabElecticidad = false;
        animatorGato.enabled = true;
    }
}
