using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Le prefab de balle
    public Transform firePoint; // Le point d'o� la balle sort
    public float bulletSpeed = 30f;
    public float fireRate = 0.1f; // Temps entre chaque tir (0.1 = 10 balles/sec)

    private float nextFireTime = 0f;

    void Update()
    {
        // Tir avec clic gauche
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Cr�e la balle
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Donne de la vitesse � la balle
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;

        Debug.Log("arah arah");
    }
}