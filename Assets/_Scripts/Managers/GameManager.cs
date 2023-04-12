using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public SpawnManager spawnManager;
    private int maxEntites;

    [Header("UI")]
    public bool useUI;
    public TMP_Text scoreText;

    private int Score;

    private Ability selectedAbility;

    // Start is called before the first frame update
    void Start()
    {
        Score = -1;
        score();
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

    public void score()
    {
        if (useUI)
        {
            Score++;
            scoreText.text = "Harvested:\n" + Score + "/" + spawnManager.getMaxEntites();
        }
    }
}
