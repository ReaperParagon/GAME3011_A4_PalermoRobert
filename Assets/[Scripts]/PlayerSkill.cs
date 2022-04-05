using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSkill", menuName = "Custom/PlayerSkill", order = 0)]
public class PlayerSkill : ScriptableObject
{
    [Range(0, 100)]
    public int HackingLevel = 0;
}
