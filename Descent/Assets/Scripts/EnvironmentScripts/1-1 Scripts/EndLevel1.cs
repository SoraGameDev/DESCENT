using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EndLevel1 : MonoBehaviour
{
    public GameObject endScreen;
    public MouseLook ml;
    public GameObject playerWeapons;
    public TextMeshProUGUI stopwatchTime;
    public TextMeshProUGUI highscoreTime;
    public float currentTime;
    public float bestTime;
    public Level1Stopwatch stop;
    public Rigidbody rb;
    public UIManager uiManager;


    private void Update()
    {
        TimeSpan stopTime = TimeSpan.FromSeconds(currentTime);
        stopwatchTime.text = stopTime.ToString(@"mm\:ss\:fff");
        TimeSpan hsTime = TimeSpan.FromSeconds(bestTime);
        highscoreTime.text = hsTime.ToString(@"mm\:ss\:fff");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelEnd();
        }

    }

    void LevelEnd()
    {
        endScreen.SetActive(true);
        Time.timeScale = 0;
        ml.enabled = false;
        playerWeapons.SetActive(false);
        stop.stopwatchActive = false;
        currentTime = stop.currentTime;
        bestTime = PlayerPrefs.GetFloat("Level2HighScore");
        rb.isKinematic = true;
        uiManager.gamePaused = true;
        PlayerPrefs.SetInt("Level1Complete", 1);
    }
}
