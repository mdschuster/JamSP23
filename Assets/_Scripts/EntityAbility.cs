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

    public void updateAbility(Ability ability)
    {
        if(this.ability == GameManager.instance().detonator)
        {
            return;
        }
        if (this.ability == GameManager.instance().blocker)
        {
            if(ability== GameManager.instance().detonator)
            {
                this.ability = ability;
                return;
            }
            else
            {
                return;
            }
        }
        this.ability = ability;
    }

    public Ability getCurrentAbility()
    {
        return ability;
    }

    public void death()
    {
        Destroy(this.gameObject);
    }
}
