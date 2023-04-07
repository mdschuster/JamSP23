using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleton part
    private static GameManager _instance=null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager instance()
    {
        return _instance;
    }

    //*******************************************

    public Ability none;
    public Ability blocker;
    public Ability faller;
    public Ability detonator;

    private Ability selectedAbility;

    // Start is called before the first frame update
    void Start()
    {
        selectedAbility = faller;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Ability getSelectedAbility()
    {
        return selectedAbility;
    }

    //called from buttons
    public void setAbility(Ability a)
    {
        selectedAbility = a;
    }
}
