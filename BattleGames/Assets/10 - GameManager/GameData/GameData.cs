using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="GameData", menuName ="Battle Games/GameData")]
public class GameData : ScriptableObject
{
    [Header("Animation Attributes")]
    public float moveSpeed;
}
