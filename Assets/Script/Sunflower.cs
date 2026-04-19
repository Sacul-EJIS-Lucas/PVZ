// Assets/Scripts/Plants/Sunflower.cs
using UnityEngine;

public class Sunflower : PlantBase
{
    public float produceInterval = 24f;
    public GameObject sunPrefab;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= produceInterval)
        {
            ProduceSun();
            timer = 0f;
        }
    }

    void ProduceSun()
    {
        Vector3 spawnPos = transform.position + Vector3.up * 0.3f;
        GameObject sun = Instantiate(sunPrefab, spawnPos, Quaternion.identity);
        sun.GetComponent<SunObject>().targetY = transform.position.y - 0.3f;
    }
}