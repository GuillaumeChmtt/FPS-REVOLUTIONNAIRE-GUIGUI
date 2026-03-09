using UnityEngine;

public class WallJump : MonoBehaviour
{
    public float wallJumpForce = 10f; // Force du saut sur le mur
    public float wallJumpSideForce = 8f; // Force latérale (s'éloigner du mur)
    public float wallSlideSpeed = 2f; // Vitesse de glissement sur le mur
    public float wallCheckDistance = 0.7f; // Distance de détéction du mur

    private Rigidbody rb;
    private bool isWallSliding = false;
    private Vector3 wallNormal; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckWall();

        if (isWallSliding && Input.GetButtonDown("Jump"))
        {
            WallJumpOff();
        }
    }

    void FixedUpdate()
    {
        if (isWallSliding)
        {
            if (rb.linearVelocity.y < -wallSlideSpeed)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, -wallSlideSpeed, rb.linearVelocity.z);
            }
        }
    }

    void CheckWall()
    {
        RaycastHit hit;
        bool hitWall = false;

        // Devant
        if (Physics.Raycast(transform.position, transform.forward, out hit, wallCheckDistance))
        {
            hitWall = true;
            wallNormal = hit.normal;
        }
        // Droite
        else if (Physics.Raycast(transform.position, transform.right, out hit, wallCheckDistance))
        {
            hitWall = true;
            wallNormal = hit.normal;
        }
        // Gauche
        else if (Physics.Raycast(transform.position, -transform.right, out hit, wallCheckDistance))
        {
            hitWall = true;
            wallNormal = hit.normal;
        }

        CodePersoPrincipal playerScript = GetComponent<CodePersoPrincipal>();
        if (hitWall && !playerScript.isGrounded && rb.linearVelocity.y < 0)
        {
            isWallSliding = true;
            Debug.DrawRay(transform.position, wallNormal * 2f, Color.green);
        }
        else
        {
            isWallSliding = false;
        }
    }

    void WallJumpOff()
    {
        rb.linearVelocity = Vector3.zero;

        Vector3 jumpDirection = wallNormal * wallJumpSideForce + Vector3.up * wallJumpForce;
        rb.AddForce(jumpDirection, ForceMode.Impulse);

        isWallSliding = false;

        Debug.Log("Wall Jump!");
    }

    void OnDrawGizmos()
    {
        if (isWallSliding)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}