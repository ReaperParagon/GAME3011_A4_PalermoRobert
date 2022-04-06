using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HackingTile))]
public class OutputTile : MonoBehaviour
{
    private HackingTile outputTile;

    private void Awake()
    {
        outputTile = GetComponent<HackingTile>();
    }

    private void LateUpdate()
    {
        if (!HackingBoard.allowInput) return;

        if (outputTile.isPowered) HackingEvents.InvokeOnMiniGameComplete();
    }

}
