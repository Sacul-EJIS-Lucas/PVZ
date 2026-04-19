// Assets/Scripts/SunManager.cs
using UnityEngine;
using TMPro;

public class SunManager : MonoBehaviour
{
    public static SunManager Instance;

    public int sunCount = 50;
    public TextMeshProUGUI sunText;

    [Header("Auto Drop")]
    public GameObject sunPrefab;
    public float dropInterval = 10f;
    private float nextDropTime;

    void Awake() => Instance = this;

    void Start()
    {
        nextDropTime = Time.time + dropInterval;
        UpdateUI();
    }

    void Update()
    {
        if (Time.time >= nextDropTime)
        {
            SpawnSun();
            nextDropTime = Time.time + dropInterval;
        }
    }

    void SpawnSun()
    {
        float x = Random.Range(-3.5f, 3.5f);
        float targetY = Random.Range(-1.5f, 1.5f);
        Vector3 spawnPos = new Vector3(x, 5f, 0);
        GameObject sun = Instantiate(sunPrefab, spawnPos, Quaternion.identity);
        sun.GetComponent<SunObject>().targetY = targetY;
    }

    public bool CanAfford(int cost) => sunCount >= cost;

    public void SpendSun(int cost)
    {
        sunCount -= cost;
        UpdateUI();
    }

    public void AddSun(int amount)
    {
        sunCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (sunText != null) sunText.text = sunCount.ToString();
    }
}