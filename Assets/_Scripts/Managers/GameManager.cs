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
