using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HackingBoard : HackingGrid
{
    /// Inputs ///

    public void OnTap()
    {
        if (HackingTile.currentTile) HackingTile.currentTile.Rotate();
    }
}
