using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed;

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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        GetInput();

        HandleGround();
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
            Debug.Log("Should Jump");
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
}
