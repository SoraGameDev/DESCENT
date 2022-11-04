using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Level1Stopwatch : MonoBehaviour
{
    public bool stopwatchActive = true;
    public float currentTime;
    public TextMeshProUGUI currentTimeText;



    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
        if (stopwatchActive == false && currentTime < PlayerPrefs.GetFloat("Level2HighScore"))
        {
            PlayerPrefs.SetFloat("Level2HighScore", currentTime);

        }
        if (stopwatchActive == false && !PlayerPrefs.HasKey("Level2HighScore"))
        {
            PlayerPrefs.SetFloat("Level2HighScore", currentTime);
        }
    }
}
