using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class EntityAbility : MonoBehaviour
{

    [Header("Ability Data")]
    [SerializeField] private Ability ability;
    public GameObject DeathFX;

    public float getModifiedMovementSpeed()
    {
        return ability.movementSpeedModifier;
    }
    public float getModifiedFallSpeed()
    {
        return ability.fallSpeedModifier;
    }

    public bool updateAbility(Ability ability)
    {
        if (this.ability == GameManager.instance().detonator)
        {
            return false;
        }
        if (this.ability == GameManager.instance().blocker)
        {
            if(ability== GameManager.instance().detonator)
            {
                
                float fallspeed = this.ability.fallSpeedModifier;
                float movespeed = this.ability.movementSpeedModifier;
                this.ability = ability;
                this.ability.fallSpeedModifier = fallspeed;
                this.ability.movementSpeedModifier = movespeed;
                return true;
            }
            else
            {
                return false;
            }
        }

        
        if(ability == GameManager.instance().detonator)
        {
            float fallspeed = this.ability.fallSpeedModifier;
            float movespeed = this.ability.movementSpeedModifier;
            this.ability = ability;
            this.ability.fallSpeedModifier = fallspeed;
            this.ability.movementSpeedModifier = movespeed;
        }
        else if (ability == GameManager.instance().blocker)
        {
            this.gameObject.tag = "ground";
            this.gameObject.layer = LayerMask.NameToLayer("Ground");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            this.ability = ability;
        }
        else
        {
            this.ability = ability;
        }
        

        return true;


    }

    public Ability getCurrentAbility()
    {
        return ability;
    }
}
