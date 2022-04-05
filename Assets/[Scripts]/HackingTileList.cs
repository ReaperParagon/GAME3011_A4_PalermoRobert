using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileList", menuName = "Custom/TileList", order = 1)]
public class HackingTileList : ScriptableObject
{
    public List<TileInfo> Tiles = new List<TileInfo>();

    private TileInfo GetRandomItemFromList(List<TileInfo> list)
    {
        if (list.Count <= 0) return null;

        int index = Random.Range(0, list.Count);
        TileInfo item = list[index];

        return item;
    }

    public TileInfo GetRandomItem()
    {
        return GetRandomItemFromList(Tiles);
    }
}