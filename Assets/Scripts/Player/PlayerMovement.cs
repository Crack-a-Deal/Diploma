using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private enum MovementState
    {
        Walk, Run, Crouch, Jump, Fall
    }

    [Header("Movement")]
    private PlayerInputActions inputs;
    private InputAction movement;

    [SerializeField] CharacterController character;

    private float movementSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Transform orientation;
    [Space]

    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;

    private Vector3 movementDirection;
    Vector3 gravityMovement = Vector3.zero;
    private MovementState movementState;


    private void Awake()
    {
        inputs = InputManager.inputActions;
    }
    private void OnEnable()
    {
        movement = inputs.Player.Movement;
        movement.Enable();

        inputs.Player.Jump.performed += Jump;
        inputs.Player.Jump.Enable();
    }

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)){
            AudioManager.PlaySound("sound");
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            AudioManager.Pause();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.Resume();
        }
        Gravity();
        Move();
    }

    private void Move()
    {
        // Run
        if (Input.GetKey(KeyCode.LeftShift) && movementState == MovementState.Walk && IsGrounded)
        {
            movementState = MovementState.Run;
            movementSpeed = runSpeed;
        }
        else if (IsGrounded)
        {
            movementState = MovementState.Walk;
            movementSpeed = walkSpeed;
        }

        movementDirection = orientation.right * movement.ReadValue<Vector2>().x + orientation.forward * movement.ReadValue<Vector2>().y;
        character.Move(movementDirection * movementSpeed * Time.deltaTime);
    }
    // Ïðûæîê
    private void Jump(InputAction.CallbackContext obj)
    {
        if(IsGrounded)
        {
            gravityMovement.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    private float currentGravity;

    private void Gravity()
    {
        if (IsGrounded && gravityMovement.y < 0)
        {
            gravityMovement.y = -2;
            movementState = MovementState.Walk;
        }

        gravityMovement.y += gravity * Time.deltaTime;

        if (gravityMovement.y > 0)
        {
            movementState = MovementState.Jump;
        }
        if (gravityMovement.y < 0 && movementState == MovementState.Jump)
        {
            movementState = MovementState.Fall;
        }

        character.Move(gravityMovement * Time.deltaTime);
    }
    [SerializeField] private Transform groundPosition;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    private bool IsGrounded => Physics.CheckSphere(groundPosition.position, groundDistance, groundMask);

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1000, 20), $"Grounded: {IsGrounded}");
        GUI.Label(new Rect(10, 30, 1000, 20), $"Movement Vector: {movement.ReadValue<Vector2>()}");
        GUI.Label(new Rect(10, 50, 1000, 20), $"Movement State - {movementState}, Speed - {movementSpeed}");
        //GUI.Label(new Rect(10, 20, 1000, 20), "Gravity - "+gravityMovement.y.ToString());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundPosition.position, groundDistance);
    }
}
