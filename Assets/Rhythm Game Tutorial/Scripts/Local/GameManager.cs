using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool startPlaying;
    public static GameManager Instance;
    public int currentScore;
    public int currentCombo;
    public int scorePerNormalNote = 6;
    public int scorePerGoodNote = 8;
    public int scorePerPerfectNote = 10;
 
    public Text scoreText;
    public Text comboText;

    public int totalNotes=0;

    public int normalHits=0;
    public int goodHits=0;
    public int perfectHits=0;
    public int missedHits=0;

    public GameObject resultScreen;
    public Text percentHitText, normalsText, goodsText, perfectHitsText, missesText, rankText, finalScoreText;
    public AudioSource bgm;

    void Start()
    {
        Instance = this;
        scoreText.text = "Score: 0";
        comboText.text = "Combo: 0";

    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying) 
        {
            if(bgm.isPlaying)
            {
                startPlaying = true;
            }
        } else
        {
            if( bgm.time > 0f && bgm.time >= bgm.clip.length && !resultScreen.activeInHierarchy)
            {
                resultScreen.SetActive(true);
                normalsText.text = ""+normalHits;
                goodsText.text = "" + goodHits;
                perfectHitsText.text = "" + perfectHits;
                missesText.text = "" + missedHits;

                float percentHit = ((float)(totalNotes - missedHits) / totalNotes) * 100f;
                percentHitText.text = percentHit.ToString("F2") + "%";

                string rankVal = "F";

                if(percentHit == 100) rankVal ="SS";
                else if (percentHit > 95) rankVal = "S";
                else if (percentHit > 85) rankVal = "A";
                else if (percentHit > 75) rankVal = "B";
                else if (percentHit > 65) rankVal = "C";
                else if (percentHit > 55) rankVal = "D";
                else if (percentHit < 55) rankVal = "F";

                rankText.text = rankVal;
                finalScoreText.text = ""+ currentScore;

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
        totalNotes ++;
        normalHits ++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote;        
        NoteHit();        
        totalNotes ++;
        goodHits ++;

    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote;        
        NoteHit();
        totalNotes ++;
        perfectHits ++;
    }
    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        comboText.text = "Combo: 0" ;
        currentCombo = 0;
        totalNotes ++;
        missedHits++;
    }

}