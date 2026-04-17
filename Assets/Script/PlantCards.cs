// PlantCard.cs
using UnityEngine;

[System.Serializable]
public class PlantCard
{
    public string plantName;
    public GameObject plantPrefab;
    public int sunCost;
    public float cooldown;
    public Sprite cardSprite;

    [HideInInspector] public float cooldownTimer = 0f;
    [HideInInspector] public bool isReady = true;
}