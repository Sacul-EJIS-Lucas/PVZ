// WaveManager.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ZombieSpawnEntry
{
    public GameObject zombiePrefab;
    public float delay;  // 距上一个僵尸的间隔时间
    public int row;      // -1 = 随机行
}

[System.Serializable]
public class Wave
{
    public List<ZombieSpawnEntry> zombies;
    public float waveDelay = 5f;  // 波次开始前等待
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public List<Wave> waves;
    private int currentWave = 0;
    private int zombiesAlive = 0;
    private bool wavesComplete = false;

    void Awake() => Instance = this;
    void Start() => StartCoroutine(RunWaves());

    IEnumerator RunWaves()
    {
        foreach (var wave in waves)
        {
            yield return new WaitForSeconds(wave.waveDelay);
            yield return StartCoroutine(SpawnWave(wave));
            // 等待该波全部死亡
            yield return new WaitUntil(() => zombiesAlive == 0);
        }
        wavesComplete = true;
        GameManager.Instance.Victory();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var entry in wave.zombies)
        {
            yield return new WaitForSeconds(entry.delay);
            int row = entry.row < 0 ? Random.Range(0, GridManager.Instance.rows) : entry.row;
            Vector3 spawnPos = new Vector3(8f, GridManager.Instance.GridToWorld(0, row).y, 0);
            Instantiate(entry.zombiePrefab, spawnPos, Quaternion.identity);
            zombiesAlive++;
        }
    }

    public void OnZombieDied() => zombiesAlive = Mathf.Max(0, zombiesAlive - 1);
}