using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Desactivacion : MonoBehaviour
{
    public MainPlayer_Interactions player;
    [SerializeField] public NavMeshAgent navmeshEnemigo;
    public float tiempoDesactivacion;
    public EnemyMovement Movement;
    public GameObject MensajePresionaMouse;
    [SerializeField] private GameObject prefabElectricidad;
    private bool prefabElecticidad =false;
    [SerializeField] private Animator animatorGato;

    void Update()
    {
    
    }

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
        animatorGato.enabled = false;
        prefabElecticidad = true;
        GameObject rayosDesactivados= Instantiate(prefabElectricidad, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(tiempoDesactivacion);
        Destroy(rayosDesactivados);
        Movement.enabled = true;
        navmeshEnemigo.enabled = true;
        prefabElecticidad = false;
        animatorGato.enabled = true;
    }
}
