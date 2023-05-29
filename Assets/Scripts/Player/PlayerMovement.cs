using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private enum MovementState
    {
        Walk, Run, Crouch, Jump, Fall, Pad, Push
    }

    private PlayerInputActions inputs;
    private InputAction movement;

    [Header("Movement")]
    [SerializeField] CharacterController character;
    private float movementSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Transform orientation;

    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float blueJumpHeight;
    [SerializeField] private float gravity;

    [Header("Grounded")]
    [SerializeField] private Transform groundPosition;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;

    [Header("Test")]
    public bool canMove = true;
    public bool canJump = true;

    private Vector3 movementDirection;
    Vector3 gravityMovement = Vector3.zero;
    private MovementState movementState;


    private void Awake()
    {
        inputs = new PlayerInputActions();

    }
    private void OnEnable()
    {
        movement = inputs.Player.Movement;
        movement.Enable();

        inputs.Player.Jump.performed += Jump;
        inputs.Player.Jump.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
        inputs.Player.Jump.Disable();
    }



    void Update()
    {
        if(movementState == MovementState.Pad) {
            gravityMovement.y = Mathf.Sqrt(blueJumpHeight * -2 * gravity);
        }
        Gravity();
        Move();
    }

    // Функция реализует управление персонажем
    private void Move()
    {
        if (!canMove)
            return;

        if (Input.GetKey(KeyCode.LeftShift) && movementState == MovementState.Walk && IsGrounded)
        {
            movementState = MovementState.Run;
            movementSpeed = runSpeed;
        }
        else if (IsGrounded && movementState != MovementState.Pad && movementState != MovementState.Push)
        {
            movementState = MovementState.Walk;
            movementSpeed = walkSpeed;
        }

        if(IsGrounded && movementDirection != Vector3.zero)
        {
            int x = UnityEngine.Random.Range(1, 6);
            string sound = $"step_dirt_0{x}";
            AudioManager.PlayFootSteps(sound);
        }
        
        movementDirection = orientation.right * movement.ReadValue<Vector2>().x + orientation.forward * movement.ReadValue<Vector2>().y;
        character.Move(movementDirection * movementSpeed * Time.deltaTime);
    }
    // Функция реализует прыжок персонажа
    private void Jump(InputAction.CallbackContext obj)
    {
        if(!canJump)
            return;

        if (groundPosition == null)
        {
            Debug.LogWarning("NO ground");
            return;
        }
        if (IsGrounded)
        {
            AudioManager.PlaySound("player_jump");
            gravityMovement.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    // Функци реализует гравитацию и движение персонажа в пространстве
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

    public bool IsGrounded => Physics.CheckSphere(groundPosition.position, groundDistance, groundMask);

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Blue")
        {
            movementState = MovementState.Pad;
        }
    }
    private void OnGUI()
    {
        if (InputManager.isDev)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, new Color(0f, 0f, 0f, .5f));
            texture.Apply();

            GUI.DrawTexture(new Rect(0, 0, 250, 200), texture);
            GUI.Label(new Rect(10, 10, 1000, 20), $"Grounded: {IsGrounded}");
            GUI.Label(new Rect(10, 30, 1000, 20), $"Movement Vector: {movement.ReadValue<Vector2>()}");
            GUI.Label(new Rect(10, 50, 1000, 20), $"Movement State - {movementState}, Speed - {movementSpeed}");
            //GUI.Label(new Rect(10, 20, 1000, 20), "Gravity - "+gravityMovement.y.ToString());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundPosition.position, groundDistance);
    }
}
