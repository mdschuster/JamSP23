using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public Canvas GameOverCanvas;
    public TMP_Text FinalScoreText;

    private int Score;

    private Ability selectedAbility;

    public GameObject DoorAudioObject;

    // Start is called before the first frame update
    void Start()
    {
        //play door opening sound
        Instantiate(DoorAudioObject, this.transform.position, Quaternion.identity);
        GameOverCanvas.gameObject.SetActive(false);
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

    public void onReturnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        GameOverCanvas.gameObject.SetActive(true);
        FinalScoreText.text = "" + Score + "/" + spawnManager.getMaxEntites();
    }
}
