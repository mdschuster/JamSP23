using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{

    private EntityAbility ability;
    private EntityMovement movement;




    // Start is called before the first frame update
    void Start()
    {
        ability = GetComponent<EntityAbility>();
        movement = GetComponent<EntityMovement>();
    }


    private void OnMouseDown()
    {
        Ability selectedAbility = GameManager.instance().getSelectedAbility();
        ability.updateAbility(selectedAbility);
    }


}
