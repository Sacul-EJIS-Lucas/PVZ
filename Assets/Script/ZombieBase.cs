// Assets/Scripts/Zombies/ZombieBase.cs
using UnityEngine;

public class ZombieBase : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 0.4f;

    [Header("Attack")]
    public float attackInterval = 1f;
    public int   attackDamage   = 100;

    protected enum ZombieState { Moving, Eating, Dead }
    protected ZombieState state = ZombieState.Moving;

    protected PlantBase currentTarget;
    protected float     attackTimer;
    protected Animator  anim;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (state == ZombieState.Dead) return;

        if (state == ZombieState.Moving)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x < -6f)
                GameManager.Instance.GameOver();
        }
        else if (state == ZombieState.Eating)
        {
            if (currentTarget == null)
            {
                SetState(ZombieState.Moving);
                return;
            }
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                attackTimer = 0f;
                currentTarget?.TakeDamage(attackDamage);
                anim?.SetTrigger("Bite");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (state == ZombieState.Dead) return;
        if (!other.CompareTag("Plant")) return;
        if (Mathf.Abs(transform.position.y - other.transform.position.y) > 0.45f) return;

        var plant = other.GetComponent<PlantBase>();
        if (plant != null)
        {
            currentTarget = plant;
            SetState(ZombieState.Eating);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plant") &&
            other.GetComponent<PlantBase>() == currentTarget)
        {
            currentTarget = null;
            if (state != ZombieState.Dead)
                SetState(ZombieState.Moving);
        }
    }

    protected void SetState(ZombieState newState)
    {
        state = newState;
        switch (newState)
        {
            case ZombieState.Moving:
                currentTarget = null;
                attackTimer   = 0f;
                anim?.SetBool("IsEating", false);
                break;
            case ZombieState.Eating:
                attackTimer = 0f;
                anim?.SetBool("IsEating", true);
                break;
            case ZombieState.Dead:
                anim?.SetTrigger("Die");
                break;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        GetComponent<ZombieHealth>().TakeDamage(damage);
    }

    public virtual void Die()
    {
        if (state == ZombieState.Dead) return;
        SetState(ZombieState.Dead);
        WaveManager.Instance?.OnZombieDied();
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.2f);
    }
}