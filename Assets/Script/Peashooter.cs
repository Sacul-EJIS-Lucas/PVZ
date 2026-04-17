// Peashooter.cs
using UnityEngine;

public class Peashooter : PlantBase
{
    public float shootInterval = 1.5f;
    public GameObject peaPrefab;
    private float timer;

    void Update()
    {
        if (HasZombieInRow())
        {
            timer += Time.deltaTime;
            if (timer >= shootInterval)
            {
                Shoot();
                timer = 0;
            }
        }
    }

    bool HasZombieInRow()
    {
        // 检测同行右侧是否有僵尸
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position + Vector3.right * 5f,
            new Vector2(10f, 0.8f),
            0
        );
        foreach (var hit in hits)
            if (hit.CompareTag("Zombie")) return true;
        return false;
    }

    void Shoot()
    {
        Vector3 pos = transform.position + Vector3.right * 0.6f;
        Instantiate(peaPrefab, pos, Quaternion.identity);
    }
}