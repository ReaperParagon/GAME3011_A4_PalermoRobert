using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackingBoard : HackingGrid
{
    private List<HackingTile> poweredTiles = new List<HackingTile>();

    private void Update()
    {
        if (!allowInput) return;

        UnPowerTiles();
        GetTileAtPosition(0, 0).PowerTile(true);
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
    }

    /// Inputs ///

    public void OnTap()
    {
        if (!allowInput) return;

        if (HackingTile.currentTile) HackingTile.currentTile.Rotate();
    }
}
