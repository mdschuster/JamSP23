using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class EntityAbility : MonoBehaviour
{

    [Header("Ability Data")]
    [SerializeField] private Ability ability;

    public float getModifiedMovementSpeed()
    {
        return ability.movementSpeedModifier;
    }
    public float getModifiedFallSpeed()
    {
        return ability.fallSpeedModifier;
    }
}
