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
using UnityEngine.UI;
using TMPro;


public class Entity : MonoBehaviour
{

    private EntityAbility ability;
    private EntityMovement movement;
    public TMP_Text TimerText;
    public Canvas TimerCanvas;
    public LayerMask layerMask;

    private float timer;

    public System.Action timerEndFunction;

    [Header("Animation")]
    public Animator animator;
    public GameObject graphic;
    public bool falling;
    public bool blocking;
    public bool exploding;
    public bool walking;


    private float fallTime;
    public System.Action<GameObject> onDeathAction;

    [Header("Audio")]
    public GameObject SplatAudioObject;
    public GameObject PopAudioObject;




    // Start is called before the first frame update
    void Start()
    {
        fallTime = 0;
        falling = true;
        animator.SetBool("Falling", true);
        TimerCanvas.gameObject.SetActive(false);
        ability = GetComponent<EntityAbility>();
        movement = GetComponent<EntityMovement>();
        movement.DirectionAction += changeDirection;
    }

    private void Update()
    { 
        if (!movement.getGrounded())
        {
            falling = true;
            animator.SetBool("Falling", true);
            if (ability.getCurrentAbility() == GameManager.instance().faller)
            {
                animator.SetBool("Umbrella", true);
            }
            fallTime += Time.deltaTime;
        } else
        {
            if (fallTime >= movement.maxFallTime)
            {
                if (ability.getCurrentAbility().fallSpeedModifier > 0.75)
                {
                    death();
                }
            }
            falling = false;
            fallTime = 0;
            animator.SetBool("Umbrella", false);
            animator.SetBool("Falling", false);
        }

        if (ability.getCurrentAbility().hasTimer)
        {
            if (timer <= 0)
            {
                TimerCanvas.gameObject.SetActive(false);

                timerEndFunction?.Invoke();

            }
            else
            {
                TimerText.text = Mathf.CeilToInt(timer).ToString();
                timer -= Time.deltaTime;
            }
        } 
    }


    private void OnMouseDown()
    {
        Ability selectedAbility = GameManager.instance().getSelectedAbility();
        bool changed=ability.updateAbility(selectedAbility);
        if (changed)
        {
            if (selectedAbility == GameManager.instance().blocker)
            {
                animator.SetBool("Umbrella", false);
                animator.SetBool("Blocking", true);
                blocking = true;
                walking = false;
                falling = false;
                exploding = false;
            }

        }
        if (selectedAbility.hasTimer)
        {
            TimerCanvas.gameObject.SetActive(true);
            timer = selectedAbility.time;
        }
        if (selectedAbility == GameManager.instance().detonator)
        {
            timerEndFunction += death;
        }
    }

    private void changeDirection()
    {
        graphic.transform.RotateAround(transform.position, transform.up, 180f);
    }

    public void death()
    {

        onDeathAction?.Invoke(this.gameObject);
        Instantiate(ability.DeathFX, this.transform.position, Quaternion.identity);
        if (ability.getCurrentAbility() == GameManager.instance().detonator)
        {
            Instantiate(PopAudioObject, this.transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(SplatAudioObject, this.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

}
