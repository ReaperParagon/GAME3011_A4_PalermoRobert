using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingGrid : MonoBehaviour
{
    public List<Vector2Int> GridSizes = new List<Vector2Int>();

    protected Vector2Int GridDimensions;
    protected int GridCount { get { return GridDimensions.x * GridDimensions.y; } }
    protected List<List<HackingTile>> GridTiles = new List<List<HackingTile>>();

    [Header("Grid Tile Information")]
    public HackingTileList tilePrefabList;
    public GameObject boardObject;
    [SerializeField]
    protected GameObject TilePrefab;

    protected List<HackingTile> tileList = new List<HackingTile>();

    public static bool allowInput { private set; get; } = false;
    protected DifficultyLevel currentDifficulty = DifficultyLevel.Easy;

    protected void OnEnable()
    {
        HackingEvents.MiniGameStart += Setup;
    }

    protected void OnDisable()
    {
        HackingEvents.MiniGameStart -= Setup;
    }

    protected List<HackingTile> GetCloseTiles(Vector2Int gridPosition)
    {
        List<HackingTile> rTileList = new List<HackingTile>();

        int x = gridPosition.x;
        int y = gridPosition.y;

        // Left
        rTileList.Add(x - 1 >= 0 ? GetTileAtPosition(x - 1, y) : null);

        // Top
        rTileList.Add(y > 0 ? GetTileAtPosition(x, y - 1) : null);

        // Right
        rTileList.Add(x + 1 <= GridTiles.Count - 1 ? GetTileAtPosition(x + 1, y) : null);

        // Bottom
        rTileList.Add(y < GridTiles[0].Count - 1 ? GetTileAtPosition(x, y + 1) : null);

        return rTileList;
    }

    protected HackingTile GetTileAtPosition(int x, int y)
    {
        return GridTiles[x][y];
    }

    protected void ResetVisited()
    {
        foreach (List<HackingTile> list in GridTiles)
        {
            foreach (HackingTile tile in list)
            {
                tile.wasVisited = false;
            }
        }
    }

    protected void Setup(DifficultyLevel difficulty, PlayerSkill playerSkill)
    {
        currentDifficulty = difficulty;
        GridDimensions = GridSizes[(int)difficulty];

        // Destroy grid
        for (int i = boardObject.transform.childCount - 1; i >= 0; i--)
            Destroy(boardObject.transform.GetChild(i).gameObject);

        // Setup Grid Layout
        GridLayoutGroup gridLayout = boardObject.GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = GridDimensions.x;

        for (int i = 0; i < GridCount; i++)
        {
            // Add Tile to Grid
            GameObject tile = Instantiate(TilePrefab, boardObject.transform);

            // Store info based on all grid positions
            int gridY = i / GridDimensions.x;
            int gridX = i % GridDimensions.x;

            HackingTile matchTile = tile.GetComponent<HackingTile>();

            matchTile.Init(tile, new Vector2Int(gridX, gridY), tilePrefabList.GetRandomItem());

            // Check our Grid size
            if (gridX > GridTiles.Count - 1)
                GridTiles.Add(new List<HackingTile>());

            if (gridY > GridTiles[gridX].Count - 1)
                GridTiles[gridX].Add(matchTile);

            GridTiles[gridX][gridY] = matchTile;
        }

        for (int row = 0; row < GridTiles.Count; row++)
        {
            for (int col = 0; col < GridTiles[row].Count; col++)
            {
                HackingTile tile = GridTiles[row][col];

                // Set up neighbouring tiles
                tile.CloseTiles.AddRange(GetCloseTiles(tile.GridPosition));
            }
        }

        allowInput = true;
    }
}
