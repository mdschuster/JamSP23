/*
Copyright 2023 Micah Schuster

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation and/or
other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
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
