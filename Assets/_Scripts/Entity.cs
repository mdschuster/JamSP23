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




    // Start is called before the first frame update
    void Start()
    {
        TimerCanvas.gameObject.SetActive(false);
        ability = GetComponent<EntityAbility>();
        movement = GetComponent<EntityMovement>();
    }

    private void Update()
    {
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
        ability.updateAbility(selectedAbility);
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

}
