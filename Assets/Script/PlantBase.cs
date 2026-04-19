// Assets/Scripts/Plants/PlantBase.cs
using UnityEngine;

public class PlantBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 300;
    public int currentHealth;

    [HideInInspector] public int gridCol;
    [HideInInspector] public int gridRow;

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
        Destroy(gameObject);
    }
}