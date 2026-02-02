using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Si touche quelque chose la balle disparaît
        Debug.Log("Bullet hit: " + other.name);
        Destroy(gameObject);
    }
}