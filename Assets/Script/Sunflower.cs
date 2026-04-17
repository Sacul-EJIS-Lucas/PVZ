// Sunflower.cs
using UnityEngine;

public class Sunflower : PlantBase
{
    public float produceInterval = 24f;
    public GameObject sunPrefab;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= produceInterval)
        {
            ProduceSun();
            timer = 0;
        }
    }

    void ProduceSun()
    {
        Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
        GameObject sun = Instantiate(sunPrefab, spawnPos, Quaternion.identity);
        sun.GetComponent<SunObject>().targetY = transform.position.y;
    }
}