using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using FMODUnity;

public class UIManager : MonoBehaviour
{
    
    [Header("References")]
    public GameObject MenuConfirmation;
    public GameObject player;
    public GameObject playerCam;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject audioMenu;
    public GameObject controlsMenu;
    public GameObject graphicsMenu;
    public TextMeshProUGUI speedText;  
    public StrafeMovement sMovement;
    public Rigidbody rb;
    public GameObject fpsCount;
    public GameObject speedCount;
    public Toggle fpsToggle;
    public Toggle speedToggle;
    FMOD.Studio.Bus MasterBus;
    [Header("Variables")]
    public float speed;
    public bool gamePaused = false;
    public int QualityIndex;

  
    
    
 
    void Start()
    {
        // Locks the cursor into the game window
        Cursor.lockState = CursorLockMode.Locked;

        MasterBus = RuntimeManager.GetBus("bus:/");
       
        
        // Player Prefs Management
       if (!PlayerPrefs.HasKey("MouseSensX"))
        {
            PlayerPrefs.SetFloat("MouseSensX", 1f);
        }
        if (!PlayerPrefs.HasKey("MouseSensY"))
        {
            PlayerPrefs.SetFloat("MouseSensY", 1f);
        }
        if (!PlayerPrefs.HasKey("ShowSpeed"))
        {
            PlayerPrefs.SetInt("ShowSpeed", 0);
        }
        if (!PlayerPrefs.HasKey("ShowFPS"))
        {
            PlayerPrefs.SetInt("ShowFPS", 0);
        }
        if (PlayerPrefs.GetInt("ShowSpeed") == 0)
        {
            speedCount.SetActive(false);
            speedToggle.isOn = false;
        }
        if (PlayerPrefs.GetInt("ShowSpeed") == 1)
        {
            speedCount.SetActive(true);
            speedToggle.isOn = true;
        }
        if (PlayerPrefs.GetInt("ShowFPS") == 0)
        {
            fpsCount.SetActive(false);
            fpsToggle.isOn = false;
        }
        if (PlayerPrefs.GetInt("ShowFPS") == 1)
        {
            fpsCount.SetActive(true);
            fpsToggle.isOn = true;
        }

    }


    void Update()
    {
        // Pause Menu Function
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
        }


        // Pause functionality
        if(gamePaused == true){
            PauseGame();
            
            playerCam.GetComponent<MouseLook>().enabled = false;
            RuntimeManager.MuteAllEvents(true);

            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartLevel();
            }

        }
        if(gamePaused == false)
        {
            ResumeGame();
            
            playerCam.GetComponent<MouseLook>().enabled = true;
            settingsMenu.SetActive(false);
            RuntimeManager.MuteAllEvents(false);
        }

        if (PlayerPrefs.GetInt("ShowSpeed") == 0)
        {
            speedCount.SetActive(false);
          //  speedToggle.enabled = false;
        }
        if (PlayerPrefs.GetInt("ShowSpeed") == 1)
        {
            speedCount.SetActive(true);
           // speedToggle.enabled = true;
        }
        if (PlayerPrefs.GetInt("ShowFPS") == 0)
        {
            fpsCount.SetActive(false);
           // fpsToggle.enabled = false;
        }
        if (PlayerPrefs.GetInt("ShowFPS") == 1)
        {
            fpsCount.SetActive(true);
            //fpsToggle.enabled = true;
        }

    }

    private void FixedUpdate()
    {
        //Speed Counter
        speedText.text = sMovement.playerSpeed.ToString();
        
    }

    // Button functions outside of the main menu
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MainMenuConfirmation()
    {
        MenuConfirmation.SetActive(true);

    }

    public void MainMenuPressNo()
    {
        MenuConfirmation.SetActive(false);
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

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void GoToNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentScene + 1);
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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

    public void MouseSensitivityX(float newMouseSensX)
    {
        PlayerPrefs.SetFloat("MouseSensX", newMouseSensX);
    }
    public void MouseSensitivityY(float newMouseSensY)
    {
        PlayerPrefs.SetFloat("MouseSensY", newMouseSensY);
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void ShowFPS(bool showFPS)
    {
        if (showFPS == false)
        {
            PlayerPrefs.SetInt("ShowFPS", 0);
        }
        if (showFPS == true)
        {
            PlayerPrefs.SetInt("ShowFPS", 1);
        }
        
    }

    public void ShowSpeed(bool showSpeed)
    {
        if (showSpeed == false)
        {
            PlayerPrefs.SetInt("ShowSpeed", 0);
        }
        if (showSpeed == true)
        {
            PlayerPrefs.SetInt("ShowSpeed", 1);
        }
    }
}
