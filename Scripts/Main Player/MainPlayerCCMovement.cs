using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;


public class MainPlayerCCMovement : MonoBehaviour
{
    
    [Header("Velocity")]
    private float speed;
    public float walkingSpeed = 10f;
    public float crouchSpeed = 5f;
    public float runningSpeed = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float groundDistance = 0.4f;

    Vector3 velocity;
    bool isGrounded;
    private bool isCrouching;

    [Header("State")]
    public MovementState movementState;

    [Header("Objects")]
    public CharacterController characterController;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public Transform orientation;

    private float rayDistance; 
    private float defaultControllerHeight;

    private void Start()
    {
        rayDistance =characterController.height * 0.5f + characterController.radius; 
        defaultControllerHeight = characterController.height; 
    }
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Movement();
        ExtraMovement();
        StateHandler();
        
    }

    public void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = orientation.right * x + orientation.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
    }

    public void ExtraMovement()
    {

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            FindObjectOfType<AudioManager>().Play("Salto");
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isCrouching) 
            {
                isCrouching = true; 
            }

            else
            {

                if (CanGetUp())
                {
                    isCrouching = false;
                }
            }

            StartCoroutine(MoveToCrouchCo());

            StopCoroutine(MoveToCrouchCo());
        }

    }
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
    }

    private void StateHandler()
    {
        if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            movementState = MovementState.sprinting;
            speed = runningSpeed;
        }

        else if (isGrounded && !isCrouching)
        {
            movementState = MovementState.walking;
            speed = walkingSpeed;
            
        }

        if (isCrouching == true)
        {
            movementState = MovementState.crouching;
            speed = crouchSpeed;
        }
    }

    private bool CanGetUp()
    {
        Ray _groundRay = new Ray(transform.position, transform.up);

        RaycastHit _groundHit;

        if (Physics.SphereCast(_groundRay, characterController.radius, out _groundHit, whatIsGround))
        {
            if (Vector3.Distance(transform.position, _groundHit.point) < 2f)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator MoveToCrouchCo()
    {
        characterController.height = isCrouching ? characterController.height / 2.5f : defaultControllerHeight;

        characterController.center = new Vector3(0f, characterController.height / 12f);

        yield return null;
    }

}
