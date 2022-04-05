using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackingBoard : HackingGrid
{
    private List<HackingTile> poweredTiles = new List<HackingTile>();

    [Header("Input and Output")]
    [SerializeField] private HackingTile inputTile;
    [SerializeField] private HackingTile outputTile;

    private void Update()
    {
        if (!allowInput) return;

        inputTile.transform.position = inputTile.CloseTiles[(int)CloseTilePositions.Right].transform.position;
        outputTile.transform.position = outputTile.CloseTiles[(int)CloseTilePositions.Left].transform.position;

        UnPowerTiles();
        inputTile.PowerTile(true);
    }

    private void UnPowerTiles()
    {
        for (int i = 0; i < poweredTiles.Count; i++)
        {
            poweredTiles[i].PowerTile(false);
            poweredTiles[i].wasVisited = false;
        }
    }

    public void AddPoweredTile(HackingTile tile)
    {
        if (poweredTiles.Contains(tile)) return;

        poweredTiles.Add(tile);
    }

    protected override void Setup(DifficultyLevel difficulty, PlayerSkill playerSkill)
    {
        poweredTiles.Clear();

        base.Setup(difficulty, playerSkill);

        AddInputOutput();
    }

    private void AddInputOutput()
    {
        // Attached input and output to grid
        inputTile.CloseTiles.Clear();
        inputTile.CloseTiles = new List<HackingTile>( new HackingTile[4] );
        outputTile.CloseTiles.Clear();
        outputTile.CloseTiles = new List<HackingTile>( new HackingTile[4] );

        int inputY = Random.Range(0, GridDimensions.y - 1);
        int outputY = Random.Range(0, GridDimensions.y - 1);

        HackingTile firstTile = GetTileAtPosition(0, inputY);
        AttachTiles(inputTile, firstTile, CloseTilePositions.Right);

        HackingTile lastTile = GetTileAtPosition(GridDimensions.x - 1, outputY);
        AttachTiles(lastTile, outputTile, CloseTilePositions.Right);
    }

    private void AttachTiles(HackingTile thisTile, HackingTile thatTile, CloseTilePositions dir)
    {
        thisTile.CloseTiles[(int)dir] = thatTile;
        thatTile.CloseTiles[(int)TilePositionsHelper.GetOppositePosition(dir)] = thisTile;
    }

    /// Inputs ///

    public void OnTap()
    {
        if (!allowInput) return;

        if (HackingTile.currentTile) HackingTile.currentTile.Rotate();
    }
}
