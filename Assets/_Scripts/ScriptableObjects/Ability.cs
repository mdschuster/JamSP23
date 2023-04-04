using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Ability Data")]
public class Ability : ScriptableObject
{
    [Header("Movement Modifiers")]
    public float movementSpeedModifier;
    public float fallSpeedModifier;
}
