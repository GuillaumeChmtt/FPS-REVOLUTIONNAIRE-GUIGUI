using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CodePersoPrincipal : MonoBehaviour
{
    public float speed = 20f;
    public float jumpForce = 10f;
    public float gravity = -20f;
    public bool isGrounded = false;
    public Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false; 
    }

    void Update()
    {
        // Saut 
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ += 1f;
        if (Input.GetKey(KeyCode.S)) moveZ -= 1f;
        if (Input.GetKey(KeyCode.A)) moveX -= 1f;
        if (Input.GetKey(KeyCode.D)) moveX += 1f;

        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.y = 0;
        move.Normalize();

        Vector3 newPosition = rb.position + move * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        if (!isGrounded)
        {
            Vector3 newVelocity = rb.linearVelocity;
            newVelocity.y += gravity * Time.fixedDeltaTime;
            rb.linearVelocity = newVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}