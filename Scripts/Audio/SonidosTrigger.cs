using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosTrigger : MonoBehaviour
{
    [SerializeField] private AudioManager audios;
    [SerializeField] private GameObject miniControlsMenu;
    [SerializeField] private GameObject miniObjectivesMenu;
    public AudioSource puertaNoAbre;

    //bools audios
    private bool reproduccionLinea3 = false;
    private bool reproduccionLinea5 = false;
    private bool reproduccionLinea12 = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tele")
        {
            audios.Play("Tele");
        }

        if (other.gameObject.tag == "PuertaCerrada")
        {
            puertaNoAbre.Play();
        }

        if (other.gameObject.tag == "Puerta1")
        {
            miniControlsMenu.SetActive(false);
            miniObjectivesMenu.SetActive(false);
        }

        if (other.gameObject.tag == "AudioSeguro" && !reproduccionLinea3)
        {
            audios.Play("Linea3");
            reproduccionLinea3 = true;
        }

        if (other.gameObject.tag == "Puerta3" && !reproduccionLinea5)
        {
            audios.Play("Linea5");
            reproduccionLinea5 = true;
        }

        if (other.gameObject.tag == "Puerta6" && !reproduccionLinea12)
        {
            audios.Play("Linea12");
            reproduccionLinea12 = true;
        }
    }
}
