using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool startPlaying;
    public static GameManager instance;
    public int currentScore;
    public int currentCombo;
    public int scorePerNormalNote = 6;
    public int scorePerGoodNote = 8;
    public int scorePerPerfectNote = 10;
 
    public Text scoreText;
    public Text comboText;
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        comboText.text = "Combo: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying) 
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
            }
        }
    }

    public void NoteHit()
    {
        // currentScore += scorePerNote;        
        currentCombo ++; 
        scoreText.text = "Score: " + currentScore;
        comboText.text = "Combo: " + currentCombo;
    }

    public void NormalHit()
    {
        currentScore += scorePerNormalNote;        
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote;        
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote;        
        NoteHit();
    }
    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        comboText.text = "Combo: 0" ;
        currentCombo = 0;
    }

}
