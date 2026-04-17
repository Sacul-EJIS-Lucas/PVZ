// ZombieBase.cs
using UnityEngine;

public class ZombieBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public float moveSpeed = 0.4f;
    public int attackDamage = 10;
    public float attackInterval = 1f;

    protected PlantBase targetPlant;
    protected float attackTimer;
    protected bool isDead;

    protected virtual void Awake() => currentHealth = maxHealth;

    protected virtual void Update()
    {
        if (isDead) return;

        if (targetPlant != null)
            Attack();
        else
            Move();

        // 到达左边界 = 游戏失败
        if (transform.position.x < -6f)
            GameManager.Instance.GameOver();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    protected virtual void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            targetPlant?.TakeDamage(attackDamage);
            attackTimer = 0;
            if (targetPlant == null || targetPlant.currentHealth <= 0)
                targetPlant = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plant"))
            targetPlant = other.GetComponent<PlantBase>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plant") && targetPlant == other.GetComponent<PlantBase>())
            targetPlant = null;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        isDead = true;
        WaveManager.Instance.OnZombieDied();
        // TODO: 死亡动画
        Destroy(gameObject, 0.5f);
    }
}