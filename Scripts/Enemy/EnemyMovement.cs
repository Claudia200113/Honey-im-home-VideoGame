using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum AI_State
{
    IDLE, PATROLLING, CHASING, ATTACKING
}

public class EnemyMovement : MonoBehaviour
{
    public AI_State currentAIState;

    [Header("Idle")]
    public float waitAtPoint = 2f;
    [SerializeField]
    private Transform[] patrolPoints;
    private int currentPatrolPoint;

    [Header("Attack")]
    private float waitCounter;
    public float attackRange;

    private NavMeshAgent navAgent;

    [Header("Detection")]
    public float radius;
    public float angle;

    public bool canSeePlayer;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    //anim
    private Animator animacionGato;

    //sound
    [SerializeField] private GameObject sonido;

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        float _distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeePlayer = true;                 
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
        }
            else if (canSeePlayer)
                canSeePlayer = false;  
    }

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animacionGato = GetComponent<Animator>();
    }

    private void Start()
    {
        waitCounter = waitAtPoint;
        
    }

    private void Update()
    {
        NavMovement();
        StartCoroutine("FOVRoutine");
        if (canSeePlayer == true)
        {
            sonido.SetActive(true);
        }
        else if(canSeePlayer == false)
        {
            sonido.SetActive(false);
        }
    }

    void NavMovement()
    {
        float _distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

        switch (currentAIState)
        {
            case AI_State.IDLE:

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }

                else
                {
                    currentAIState = AI_State.PATROLLING;

                    navAgent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    animacionGato.SetBool("isWaiting", false);
                    animacionGato.SetBool("patrolling", true);
                    animacionGato.SetBool("playerSeen", false);
                }

                if (canSeePlayer == true)
                {
                   currentAIState = AI_State.CHASING;
                }

                break;

            case AI_State.PATROLLING:

                animacionGato.SetBool("isWaiting", false);
                animacionGato.SetBool("patrolling", true);
                animacionGato.SetBool("playerSeen", false);

                if (navAgent.remainingDistance <= 0.2f)
                {             
                    currentPatrolPoint++;

                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                    currentAIState = AI_State.IDLE;
                    waitCounter = waitAtPoint;
                    animacionGato.SetBool("isWaiting", true);
                    animacionGato.SetBool("patrolling", false);
                    animacionGato.SetBool("playerSeen", false);

                }

                if (canSeePlayer == true)
                {
                    currentAIState = AI_State.CHASING;
                }
                else if (canSeePlayer == false)
                {
                    currentAIState = AI_State.IDLE;
                    waitCounter = waitAtPoint;
                }

                break;

            case AI_State.CHASING:

               animacionGato.SetBool("playerSeen", true);
                animacionGato.SetBool("patrolling", false);
                animacionGato.SetBool("isWaiting", false);

                navAgent.SetDestination(GameObject.FindWithTag("Player").transform.position);

                if (_distanceToPlayer <= attackRange)
                {
                    currentAIState = AI_State.ATTACKING;
                }


                if (canSeePlayer == false)
                {
                    currentAIState = AI_State.IDLE;

                    waitCounter = waitAtPoint;

                    navAgent.velocity = Vector3.zero;

                    navAgent.SetDestination(transform.position);

                }

                break;

            case AI_State.ATTACKING:

                animacionGato.SetBool("playerSeen", true);
                animacionGato.SetBool("patrolling", false);
                animacionGato.SetBool("isWaiting", false);

                transform.LookAt(GameObject.FindWithTag("Player").transform.position, Vector3.up);

                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                    if (_distanceToPlayer < attackRange)
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        SceneManager.LoadScene("Muerte");
                    }
                    else if (canSeePlayer == false)
                    {
                        currentAIState = AI_State.IDLE;

                        waitCounter = waitAtPoint;

                        navAgent.isStopped = false;
                    }

                break;
        }

    }

}