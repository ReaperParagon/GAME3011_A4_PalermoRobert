using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CloseTilePositions
{
    Left, Right, Top, Bottom
}

public class HackingTile : MonoBehaviour
{
    public static HackingTile currentTile;
    public static HackingBoard board;

    public Image icon;

    public GameObject Tile { get; set; }
    public Vector2Int GridPosition { get; set; }

    public List<HackingTile> CloseTiles = new List<HackingTile>();

    public bool wasVisited = false;

    private void Awake()
    {
        if (icon == null) icon = GetComponent<Image>();
        if (board == null) board = FindObjectOfType<HackingBoard>();
    }

    public void Init(GameObject _tile, Vector2Int _pos)
    {
        Tile = _tile;
        GridPosition = _pos;

        if (icon == null) icon = GetComponent<Image>();
    }

    private bool CheckIsClose(HackingTile tile)
    {
        foreach (HackingTile t in CloseTiles)
            if (tile == t) return true;

        return false;
    }

    public HackingTile GetCloseTile(CloseTilePositions pos)
    {
        return CloseTiles[(int)pos];
    }

    /// Input System ///


}
