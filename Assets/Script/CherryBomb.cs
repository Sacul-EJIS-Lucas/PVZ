// CherryBomb.cs
using UnityEngine;
using System.Collections;

public class CherryBomb : PlantBase
{
    public float fuseTime = 1.5f;
    public float explosionRadius = 1.8f;
    public int explosionDamage = 1800;
    public GameObject explosionFX;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(fuseTime);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits)
            if (hit.CompareTag("Zombie"))
                hit.GetComponent<ZombieBase>()?.TakeDamage(explosionDamage);

        if (explosionFX != null)
            Instantiate(explosionFX, transform.position, Quaternion.identity);

        Die();
    }
}