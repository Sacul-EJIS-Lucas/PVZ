// Assets/Scripts/Plants/Peashooter.cs
using UnityEngine;

public class Peashooter : PlantBase
{
    public float shootInterval = 1.4f;
    public GameObject peaPrefab;
    private float timer = 0f;

    void Update()
    {
        if (HasZombieAhead())
        {
            timer += Time.deltaTime;
            if (timer >= shootInterval)
            {
                Shoot();
                timer = 0f;
            }
        }
        else
        {
            // 没有僵尸时重置计时，保证发现僵尸后稍作延迟才射
            timer = 0f;
        }
    }

    bool HasZombieAhead()
    {
        // 向右方发射一条射线，检测同行僵尸
        // 检测范围：从植物位置向右延伸到场景右边界
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, 20f,
            LayerMask.GetMask("Zombie"));
        return hit.collider != null;
    }

    void Shoot()
    {
        Vector3 pos = transform.position + Vector3.right * 0.5f;
        Instantiate(peaPrefab, pos, Quaternion.identity);
    }
}