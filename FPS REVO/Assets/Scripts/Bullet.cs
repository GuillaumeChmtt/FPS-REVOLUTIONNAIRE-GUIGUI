using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit: " + collision.gameObject.name);

        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage();
        }

        Destroy(gameObject);
    }
}