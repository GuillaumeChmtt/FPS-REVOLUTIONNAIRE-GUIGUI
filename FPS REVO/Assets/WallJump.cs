using UnityEngine;

public class WallJump : MonoBehaviour
{
    public float wallJumpForce = 10f; // Force du saut sur le mur
    public float wallJumpSideForce = 8f; // Force latérale (s'éloigner du mur)
    public float wallSlideSpeed = 2f; // Vitesse de glissement sur le mur
    public float wallCheckDistance = 0.7f; // Distance de d�tection du mur

    private Rigidbody rb;
    private bool isWallSliding = false;
    private Vector3 wallNormal; // Direction perpendiculaire au mur

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckWall();

        // Saut depuis le mur
        if (isWallSliding && Input.GetButtonDown("Jump"))
        {
            WallJumpOff();
        }
    }

    void FixedUpdate()
    {
        // Glissement sur le mur
        if (isWallSliding)
        {
            // Limite la vitesse de chute
            if (rb.linearVelocity.y < -wallSlideSpeed)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, -wallSlideSpeed, rb.linearVelocity.z);
            }
        }
    }

    void CheckWall()
    {
        // V�rifie s'il y a un mur devant, � gauche ou � droite
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

        // Le joueur est sur un mur s'il touche un mur ET tombe (pas au sol)
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
        // Annule la v�locit� actuelle
        rb.linearVelocity = Vector3.zero;

        // Saute vers le haut ET s'�loigne du mur
        Vector3 jumpDirection = wallNormal * wallJumpSideForce + Vector3.up * wallJumpForce;
        rb.AddForce(jumpDirection, ForceMode.Impulse);

        isWallSliding = false;

        Debug.Log("Wall Jump!");
    }

    // Visualisation dans l'�diteur
    void OnDrawGizmos()
    {
        if (isWallSliding)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}