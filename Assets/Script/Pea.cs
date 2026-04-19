// Assets/Scripts/Plants/Pea.cs
using UnityEngine;

public class Pea : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 20;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > 9f) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Zombie")) return;

        // 行对齐检测，防止打到斜上方的僵尸
        if (Mathf.Abs(transform.position.y - other.transform.position.y) > 0.45f) return;

        other.GetComponent<ZombieBase>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
}