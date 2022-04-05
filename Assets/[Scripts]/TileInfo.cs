using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TileInfo
{
    public void SetInfo(TileInfo info)
    {
        icon = info.icon;
        rotation = info.rotation;
        connectedPositions = new List<CloseTilePositions>(info.connectedPositions);
    }

    public Sprite icon;
    public float rotation;

    public List<CloseTilePositions> connectedPositions = new List<CloseTilePositions>();
    
    public void RotateConnections()
    {
        for (int p = 0; p < connectedPositions.Count; p++)
        {
            int i = (int)connectedPositions[p];

            if (++i > 3) i = 0;

            connectedPositions[p] = (CloseTilePositions)i;
        }

        rotation -= 90.0f;
    }
}
