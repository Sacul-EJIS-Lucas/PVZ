// Pea.cs
using UnityEngine;

public class Pea : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 20;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > 8f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<ZombieBase>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}