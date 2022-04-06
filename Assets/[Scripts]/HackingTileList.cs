using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileList", menuName = "Custom/TileList", order = 1)]
public class HackingTileList : ScriptableObject
{
    public List<TileInfoObject> Tiles = new List<TileInfoObject>();

    [SerializeField] private List<float> TileWeights = new List<float>();

    private List<float> Weights = new List<float>();

    private void OnEnable()
    {
        Weights.Clear();
    }

    private TileInfo GetRandomItemFromList(List<TileInfoObject> list)
    {
        if (list.Count <= 0) return null;

        int index = Random.Range(0, list.Count);
        TileInfo item = list[index].tileInfo;

        return item;
    }

    public TileInfo GetRandomItem()
    {
        return GetRandomItemFromList(Tiles);
    }

    private TileInfo GetWeightedItemFromList(List<TileInfoObject> list)
    {
        if (list.Count <= 0) return null;
        
        if (Weights.Count <= 0) CreateWeightings(list);

        return GetItemFromWeight(Random.Range(0.0f, Weights[Weights.Count - 1]));
    }

    public TileInfo GetWeightedItem()
    {
        return GetWeightedItemFromList(Tiles);
    }

    private void CreateWeightings(List<TileInfoObject> list)
    {
        float runningCount = 0.0f;

        for (int i = 0; i < TileWeights.Count; i++)
        {
            runningCount += TileWeights[i];
            Weights.Add(runningCount);
        }
    }

    private TileInfo GetItemFromWeight(float weight)
    {
        if (weight < 0.0f || weight > Weights[Weights.Count - 1]) return null;

        int index = 0;

        for (int i = 0; i < Weights.Count; i++)
        {
            index = i;
            if (weight < Weights[i]) break;
        }

        TileInfo tile = Tiles[index].tileInfo;
        return tile;
    }
}
