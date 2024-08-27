using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesScript : MonoBehaviour
{
    public ObjectsActive interactuables; 
    public MainPlayer_Interactions desactivador; 
    [SerializeField] private GameObject[] objetivos;
    public DesactivacionEstatica scriptEnemigoEstatico;
    [SerializeField] private Animator animacionPuerta1;
    [SerializeField] private Animator animacionPuertaBath;

    void Update()
    {
        if (interactuables.carta1activa == true)
        {
            objetivos[0].SetActive(false);
            objetivos[1].SetActive(true);
        }

        if (desactivador.desactivadorObtenido == true)
        {
            objetivos[1].SetActive(false);
            objetivos[2].SetActive(true);
        }

        if (scriptEnemigoEstatico.enemigoEstaticoDesactivado == true)
        {
            objetivos[2].SetActive(false);
            objetivos[3].SetActive(true);
            animacionPuerta1.SetBool("AbrirPuerta1", true);
            animacionPuertaBath.SetBool("PuertaBathAbierta", true);

        }
    }
}
