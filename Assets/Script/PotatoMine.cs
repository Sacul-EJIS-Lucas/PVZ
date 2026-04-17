// PotatoMine.cs
using UnityEngine;

public class PotatoMine : PlantBase
{
    public float armTime = 14f;
    public int damage = 1800;
    private bool armed = false;

    void Start() => Invoke(nameof(BecomeArmed), armTime);

    void BecomeArmed()
    {
        armed = true;
        // TODO: 更换为激活状态的 sprite
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!armed) return;
        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<ZombieBase>()?.TakeDamage(damage);
            Die();
        }
    }
}