using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;

public class MainMenuScript : MonoBehaviour
{
    [Header("References")]
    public GameObject settingsMenu;
    public GameObject audioMenu;
    public GameObject controlsMenu;
    public GameObject graphicsMenu;
    public GameObject levelSelect;
    public GameObject launchText;
    public GameObject startgameMenu;
    public GameObject level1Block, level2Block, level3Block;
    FMOD.Studio.Bus MasterBus;
    public UIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        MasterBus = RuntimeManager.GetBus("bus:/");
        Time.timeScale = 1;
        RuntimeManager.MuteAllEvents(false);
        if (!PlayerPrefs.HasKey("FirstLaunch"))
        {
            PlayerPrefs.SetInt("FirstLaunch", 0);
        }
        if (PlayerPrefs.GetInt("FirstLaunch") == 0)
        {
            launchText.SetActive(true);
        }

        if (PlayerPrefs.GetInt("IntroComplete") == 1)
        {
            level1Block.SetActive(false);
        }else if (PlayerPrefs.GetInt("IntroComplete") == 0)
        {
            level1Block.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Level1Complete") == 1)
        {
            level2Block.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Level1Complete") == 0)
        {
            level2Block.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Level2Complete") == 1)
        {
            level3Block.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Level2Complete") == 0)
        {
            level3Block.SetActive(true);
        }

    }




    //Button Scripts
    public void StartGame()
    {
        SceneManager.LoadScene(2);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        
    }
    public void OpenStartGameMenu()
    {
        startgameMenu.SetActive(true);
    }
    public void CloseStartGameMenu()
    {
        startgameMenu.SetActive(false);
    }
    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        audioMenu.SetActive(true);
        controlsMenu.SetActive(false);
        graphicsMenu.SetActive(false);
    }
    public void CloseLaunchText()
    {
        launchText.SetActive(false);
        PlayerPrefs.SetInt("FirstLaunch", 1);
    }

    public void LoadSandbox()
    {
        SceneManager.LoadScene(8);
        
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/SoraGameDev");
    }
    public void OpenItch()
    {
        Application.OpenURL("https://soragamedev.itch.io/");
    }
    public void OpenAudioMenu()
    {
        audioMenu.SetActive(true);
        controlsMenu.SetActive(false);
        graphicsMenu.SetActive(false);
    }
    public void OpenControlsMenu()
    {
        audioMenu.SetActive(false);
        controlsMenu.SetActive(true);
        graphicsMenu.SetActive(false);
    }
    public void OpenGraphicsMenu()
    {
        audioMenu.SetActive(false);
        controlsMenu.SetActive(false);
        graphicsMenu.SetActive(true);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void OpenLevelSelect()
    {
        levelSelect.SetActive(true);
    }
    public void CloseLevelSelect()
    {
        levelSelect.SetActive(false);
    }

    //LEVEL SELECT BUTTONS

    public void OpenIntro()
    {
        SceneManager.LoadScene(3);

    }


    public void OpenLevel1()
    {
        SceneManager.LoadScene(4);
        
    }
    public void OpenLevel2()
    {
        SceneManager.LoadScene(5);
    }
    public void OpenLevel3()
    {
        SceneManager.LoadScene(6);
    }
}

