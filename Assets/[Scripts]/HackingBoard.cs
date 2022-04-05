using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingBoard : MonoBehaviour
{
    public List<Vector2Int> GridSizes = new List<Vector2Int>();

    private Vector2Int GridDimensions;
    private int GridCount { get { return GridDimensions.x * GridDimensions.y; } }
    private List<List<HackingTile>> GridTiles = new List<List<HackingTile>>();

    [Header("Grid Tile Information")]
    public GameObject boardObject;
    [SerializeField]
    private GameObject TilePrefab;

    private List<HackingTile> tileList = new List<HackingTile>();

    public static bool allowInput { private set; get; } = false;
    private DifficultyLevel currentDifficulty = DifficultyLevel.Easy;

    private void OnEnable()
    {
        HackingEvents.MiniGameStart += Setup;
    }

    private void OnDisable()
    {
        HackingEvents.MiniGameStart -= Setup;
    }

    private List<HackingTile> GetCloseTiles(Vector2Int gridPosition)
    {
        List<HackingTile> rTileList = new List<HackingTile>();

        int x = gridPosition.x;
        int y = gridPosition.y;

        // Left
        rTileList.Add(x - 1 >= 0 ? GetTileAtPosition(x - 1, y) : null);

        // Right
        rTileList.Add(x + 1 <= GridTiles.Count - 1 ? GetTileAtPosition(x + 1, y) : null);

        // Top
        rTileList.Add(y > 0 ? GetTileAtPosition(x, y - 1) : null);

        // Bottom
        rTileList.Add(y < GridTiles[0].Count - 1 ? GetTileAtPosition(x, y + 1) : null);

        return rTileList;
    }

    private HackingTile GetTileAtPosition(int x, int y)
    {
        return GridTiles[x][y];
    }

    private void ResetVisited()
    {
        foreach (List<HackingTile> list in GridTiles)
        {
            foreach (HackingTile tile in list)
            {
                tile.wasVisited = false;
            }
        }
    }

    public void Setup(DifficultyLevel difficulty)
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

            matchTile.Init(tile, new Vector2Int(gridX, gridY));

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
