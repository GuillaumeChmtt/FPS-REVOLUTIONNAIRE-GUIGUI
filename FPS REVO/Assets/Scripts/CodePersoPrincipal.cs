using UnityEngine;

public class CodePersoPrincipal : MonoBehaviour
{
    public float speed = 20f;
    public float jumpForce = 10f;
    public float gravity = -20f;
    public bool isGrounded = false;
    public Rigidbody rb;

    [Header("Slide")]
    public float slideSpeed = 35f;
    public float slideDuration = 0.8f;
    public float slideCooldown = 1f;

    private bool isSliding = false;
    private bool canSlide = true;
    private Vector3 slideDirection;

    private CapsuleCollider capsuleCollider;
    private float originalHeight;
    private Vector3 originalCenter;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;

        capsuleCollider = GetComponent<CapsuleCollider>();
        originalHeight = capsuleCollider.height;
        originalCenter = capsuleCollider.center;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            if (isSliding) StopSlide();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isSliding && canSlide)
        {
            StartCoroutine(SlideCoroutine());
        }
    }

    void FixedUpdate()
    {
        if (isSliding)
        {
            Vector3 slideVelocity = slideDirection * slideSpeed;
            slideVelocity.y = rb.linearVelocity.y;
            rb.linearVelocity = slideVelocity;
        }
        else
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
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
        }

        if (!isGrounded)
        {
            Vector3 vel = rb.linearVelocity;
            vel.y += gravity * Time.fixedDeltaTime;
            rb.linearVelocity = vel;
        }
    }

    private System.Collections.IEnumerator SlideCoroutine()
    {
        // Démarrage
        isSliding = true;
        canSlide = false;

        float moveX = Input.GetKey(KeyCode.A) ? -1f : Input.GetKey(KeyCode.D) ? 1f : 0f;
        float moveZ = Input.GetKey(KeyCode.S) ? -1f : 1f;
        slideDirection = (transform.right * moveX + transform.forward * moveZ).normalized;

        capsuleCollider.height = originalHeight / 2f;
        capsuleCollider.center = new Vector3(originalCenter.x, originalCenter.y - originalHeight / 4f, originalCenter.z);


        // Attend la durée du slide
        yield return new WaitForSeconds(slideDuration);

        // Arrêt
        StopSlide();

        // Attend le cooldown
        yield return new WaitForSeconds(slideCooldown);
        canSlide = true;
    }

    void StopSlide()
    {
        isSliding = false;
        capsuleCollider.height = originalHeight;
        capsuleCollider.center = originalCenter;

        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = false;
    }
}