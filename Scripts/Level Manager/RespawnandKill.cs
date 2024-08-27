using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnandKill : MonoBehaviour
{
    public CharacterController characterController;
    [SerializeField] private Transform respawnPoint;
    public float respawnDelay = 1f;
    bool isDead = false;
    private MainPlayerCCMovement playerScript;

    private void Start()
    {
        playerScript = FindObjectOfType<MainPlayerCCMovement>();
    }

    private void Update()
    {
        if (isDead)
        {
            characterController.enabled = false;  
        }
        else
        {
            characterController.enabled = true;
        }
    }

    public void RespawnPlayer()
    {
            StartCoroutine("RespawnPlayerCo");
    }

     public IEnumerator RespawnPlayerCo()
     {
            this.enabled = false;

            playerScript.enabled = false;

            isDead = true;

            yield return new WaitForSeconds(respawnDelay);

            SceneManager.LoadScene("Muerte");

            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;

            transform.position = respawnPoint.transform.position; 

            playerScript.enabled = true;

            this.enabled = true;

            isDead = false;

     }

}