using UnityEngine;

public class Target : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cible touchée par : " + other.name);
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}