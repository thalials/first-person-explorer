using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    GameManager gm;

    [Header("Movement")]
    public float moveSpeed;

    public Vector3 startingPosition;

    public Quaternion startingRotation;

    public Transform orientation;

    public float groundDrag;

    public float jumpForce;

    public float jumpCooldown;

    public float airMultiplier;

    bool readyToJump;

    float horizontalInput;

    float verticalInput;

    Vector3 moveDirection;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;

    public LayerMask groundMask;

    bool isGrounded;

    private void Awake()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GameManager.GetInstance();
        rb.freezeRotation = true;
        readyToJump = true;
        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = startingPosition;
    }

    private void Update()
    {
        GetInput();
        HandleGround();
        if (gm.gameState == GameManager.GameState.MENU)
        {
            ResetPosition();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        SpeedControl();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (
            Input.GetKey(KeyCode.Escape) &&
            gm.gameState == GameManager.GameState.GAME
        )
        {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }
    }

    private void HandleGround()
    {
        // Verifica se o player está em contato com o solo;
        // para isso, lança um raio para baixo (relativo ao player) e retorna
        // true se o raio cruza com a superfício de um collider dentro do range de distância especificado;
        isGrounded =
            Physics
                .Raycast(transform.position,
                Vector3.down,
                playerHeight * 0.5f + 0.2f,
                groundMask);
        if (isGrounded)
            rb.drag = groundDrag; // atrito
        else
            rb.drag = 0;
    }

    private void HandleMovement()
    {
        // recalcula a direção
        moveDirection =
            orientation.forward * verticalInput +
            orientation.right * horizontalInput;

        if (isGrounded)
        {
            rb
                .AddForce(moveDirection.normalized * moveSpeed * 10f,
                ForceMode.Force);
        }
        else if (!isGrounded)
        {
            rb
                .AddForce(moveDirection.normalized *
                moveSpeed *
                10f *
                airMultiplier,
                ForceMode.Force);
        }
    }

    // limita a velocidade do player para o valor especificado (moveSpeed)
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity =
                new Vector3(limitedVelocity.x,
                    rb.velocity.y,
                    limitedVelocity.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spaceship")
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
    }
}
