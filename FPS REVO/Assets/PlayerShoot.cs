using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;
    public float fireRate = 0.1f;
    public float maxShootDistance = 1000f; // Distance max de visée

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Lance un rayon depuis le CENTRE de l'écran (là où est le crosshair)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;

        // Vérifie si on touche quelque chose
        if (Physics.Raycast(ray, out RaycastHit hit, maxShootDistance))
        {
            // On vise le point touché
            targetPoint = hit.point;
        }
        else
        {
            // Si on ne touche rien, on vise très loin dans la direction du regard
            targetPoint = ray.GetPoint(maxShootDistance);
        }

        // Calcule la direction depuis le FirePoint vers le point visé
        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

        // Crée la balle au niveau du FirePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

        // Applique la vélocité dans la bonne direction
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * bulletSpeed;

        Debug.Log("Shot fired at: " + targetPoint);
    }
}