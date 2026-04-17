// PlantBase.cs
using UnityEngine;

public class PlantBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int gridCol, gridRow;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        GridManager.Instance.RemovePlant(gridCol, gridRow);
        // TODO: 死亡动画
        Destroy(gameObject);
    }
}