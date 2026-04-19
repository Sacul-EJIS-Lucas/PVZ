// Assets/Scripts/Zombies/ZombieHealth.cs
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    [Header("Body")]
    public int bodyMaxHP = 200;
    private int bodyHP;

    [Header("Armor（0 = 无护甲）")]
    public int armorMaxHP = 0;
    private int armorHP;

    [Header("Visuals")]
    public Sprite armorIntactSprite;   // 护甲完好
    public Sprite armorDamagedSprite;  // 护甲半血（可选，留空则跳过）
    public Sprite bodySprite;          // 护甲掉落后
    public GameObject armorObject;     // 护甲子物体（掉落动画用）

    [Header("UI（可选）")]
    public Image healthBarFill;

    private SpriteRenderer sr;
    private ZombieBase zombieBase;

    void Awake()
    {
        sr         = GetComponent<SpriteRenderer>();
        zombieBase = GetComponent<ZombieBase>();
        bodyHP     = bodyMaxHP;
        armorHP    = armorMaxHP;

        if (armorMaxHP > 0 && armorIntactSprite != null)
            sr.sprite = armorIntactSprite;
        else if (bodySprite != null)
            sr.sprite = bodySprite;
    }

    public void TakeDamage(int damage)
    {
        if (armorMaxHP > 0 && armorHP > 0)
            DamageArmor(damage);
        else
            DamageBody(damage);

        UpdateHealthBar();
    }

    void DamageArmor(int damage)
    {
        armorHP -= damage;

        // 护甲低于 50% 换受损 sprite
        if (armorDamagedSprite != null && armorHP < armorMaxHP * 0.5f && armorHP > 0)
            sr.sprite = armorDamagedSprite;

        if (armorHP <= 0)
        {
            int overflow = Mathf.Abs(armorHP);
            armorHP = 0;
            BreakArmor();
            if (overflow > 0) DamageBody(overflow);
        }
    }

    void BreakArmor()
    {
        if (bodySprite != null) sr.sprite = bodySprite;

        if (armorObject != null)
        {
            armorObject.transform.SetParent(null);
            var rb = armorObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.AddForce(new Vector2(Random.Range(-1f, 1f), 4f), ForceMode2D.Impulse);
            Destroy(armorObject, 2f);
        }
    }

    void DamageBody(int damage)
    {
        bodyHP -= damage;
        if (bodyHP <= 0)
        {
            bodyHP = 0;
            zombieBase.Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill == null) return;
        int total    = bodyMaxHP + armorMaxHP;
        int current  = bodyHP + armorHP;
        healthBarFill.fillAmount = (float)current / total;
    }
}