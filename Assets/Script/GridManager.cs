// GridManager.cs
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")]
    public int columns = 9;
    public int rows = 5;
    public float cellWidth = 1.25f;
    public float cellHeight = 1.0f;
    public Vector2 originOffset = new Vector2(-4.5f, -2f);

    private PlantBase[,] grid; // 存储每格上的植物

    void Awake()
    {
        Instance = this;
        grid = new PlantBase[columns, rows];
    }

    // 根据鼠标位置获取格子坐标
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int col = Mathf.FloorToInt((worldPos.x - originOffset.x) / cellWidth);
        int row = Mathf.FloorToInt((worldPos.y - originOffset.y) / cellHeight);
        return new Vector2Int(col, row);
    }

    // 格子坐标转世界坐标（格子中心）
    public Vector3 GridToWorld(int col, int row)
    {
        float x = originOffset.x + col * cellWidth + cellWidth / 2f;
        float y = originOffset.y + row * cellHeight + cellHeight / 2f;
        return new Vector3(x, y, 0);
    }

    public bool IsValidCell(int col, int row)
    {
        return col >= 0 && col < columns && row >= 0 && row < rows;
    }

    public bool IsCellEmpty(int col, int row)
    {
        return IsValidCell(col, row) && grid[col, row] == null;
    }

    public void PlacePlant(PlantBase plant, int col, int row)
    {
        grid[col, row] = plant;
    }

    public void RemovePlant(int col, int row)
    {
        grid[col, row] = null;
    }

    public int GetRow(float worldY)
    {
        return Mathf.FloorToInt((worldY - originOffset.y) / cellHeight);
    }
}