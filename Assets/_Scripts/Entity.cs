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
                ability.death();
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
                TimerText.text = timer.ToString("#");
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
            timerEndFunction += ability.death;
        }
    }

    private void changeDirection()
    {
        graphic.transform.RotateAround(transform.position, transform.up, 180f);
    }

}
