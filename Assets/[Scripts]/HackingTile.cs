using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CloseTilePositions
{
    Left, Top, Right, Bottom
}

public class HackingTile : MonoBehaviour
{
    public static HackingTile currentTile;
    public static HackingBoard board;

    public Image icon;
    public TileInfo tileInfo;

    public GameObject Tile { get; set; }
    public Vector2Int GridPosition { get; set; }

    public List<HackingTile> CloseTiles = new List<HackingTile>();

    public bool wasVisited = false;

    private void Awake()
    {
        if (icon == null) icon = GetComponent<Image>();
        if (board == null) board = FindObjectOfType<HackingBoard>();
    }

    public void Init(GameObject _tile, Vector2Int _pos, TileInfo _info)
    {
        Tile = _tile;
        GridPosition = _pos;

        if (icon == null) icon = GetComponent<Image>();

        tileInfo = new TileInfo();
        SetTileInfo(_info);
    }

    protected bool CheckIsClose(HackingTile tile)
    {
        foreach (HackingTile t in CloseTiles)
            if (tile == t) return true;

        return false;
    }

    public HackingTile GetCloseTile(CloseTilePositions pos)
    {
        return CloseTiles[(int)pos];
    }

    private void SwapTiles(HackingTile thisTile, HackingTile thatTile)
    {
        TileInfo info = new TileInfo();
        info.SetInfo(thatTile.tileInfo);

        thatTile.SetTileInfo(thisTile.tileInfo);
        thisTile.SetTileInfo(info);
    }

    private void SetTileInfo(TileInfo info)
    {
        tileInfo.SetInfo(info);

        // Setup for tile
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, tileInfo.rotation);
        icon.sprite = tileInfo.icon;
    }

    /// Inputs ///

    public void Rotate()
    {
        // Rotate Tile
        tileInfo.RotateConnections();

        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, tileInfo.rotation);
    }

    public void OnStartDrag()
    {
        currentTile = this;
    }

    public void OnRelease()
    {
        // If we are allowed input
        if (!HackingBoard.allowInput) return;

        // If we have a current tile and there is something in both
        if (currentTile == this || currentTile == null) return;
        if (tileInfo == null || currentTile.tileInfo == null) return;

        SwapTiles(this, currentTile);
    }

    public void OnHover()
    {
        
    }
}
