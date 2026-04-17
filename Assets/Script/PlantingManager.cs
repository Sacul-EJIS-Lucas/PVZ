// PlantingManager.cs
using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    public static PlantingManager Instance;

    public PlantCard[] plantCards;
    private PlantCard selectedCard = null;
    private GameObject ghostPlant;  // 跟随鼠标的预览植物

    void Awake() => Instance = this;

    void Update()
    {
        // 冷却计时
        foreach (var card in plantCards)
        {
            if (!card.isReady)
            {
                card.cooldownTimer -= Time.deltaTime;
                if (card.cooldownTimer <= 0)
                {
                    card.isReady = true;
                    card.cooldownTimer = 0;
                }
            }
        }

        if (selectedCard != null)
        {
            UpdateGhost();
            if (Input.GetMouseButtonDown(0))
                TryPlant();
            if (Input.GetMouseButtonDown(1))
                CancelPlanting();
        }
    }

    public void SelectCard(PlantCard card)
    {
        if (!card.isReady) return;
        if (!SunManager.Instance.CanAfford(card.sunCost)) return;

        selectedCard = card;

        if (ghostPlant != null) Destroy(ghostPlant);
        ghostPlant = Instantiate(card.plantPrefab);
        // 禁用 ghost 的脚本，只保留外观
        foreach (var script in ghostPlant.GetComponents<MonoBehaviour>())
            script.enabled = false;
        SetGhostAlpha(0.6f);
    }

    void UpdateGhost()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        Vector2Int cell = GridManager.Instance.WorldToGrid(mouseWorld);
        if (GridManager.Instance.IsValidCell(cell.x, cell.y))
            ghostPlant.transform.position = GridManager.Instance.GridToWorld(cell.x, cell.y);
    }

    void TryPlant()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        Vector2Int cell = GridManager.Instance.WorldToGrid(mouseWorld);

        if (!GridManager.Instance.IsCellEmpty(cell.x, cell.y)) return;

        SunManager.Instance.SpendSun(selectedCard.sunCost);
        Vector3 pos = GridManager.Instance.GridToWorld(cell.x, cell.y);
        GameObject plant = Instantiate(selectedCard.plantPrefab, pos, Quaternion.identity);
        plant.GetComponent<PlantBase>().gridCol = cell.x;
        plant.GetComponent<PlantBase>().gridRow = cell.y;
        GridManager.Instance.PlacePlant(plant.GetComponent<PlantBase>(), cell.x, cell.y);

        selectedCard.isReady = false;
        selectedCard.cooldownTimer = selectedCard.cooldown;
        CancelPlanting();
    }

    void CancelPlanting()
    {
        selectedCard = null;
        if (ghostPlant != null) Destroy(ghostPlant);
    }

    void SetGhostAlpha(float alpha)
    {
        var renderers = ghostPlant.GetComponentsInChildren<SpriteRenderer>();
        foreach (var r in renderers)
        {
            Color c = r.color;
            c.a = alpha;
            r.color = c;
        }
    }
}