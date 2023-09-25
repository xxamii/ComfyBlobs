using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject tint;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Back();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            tint.SetActive(false);
        }
        else
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            tint.SetActive(true);
        }

        isPaused = !isPaused;

    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Back()
    {
        if (settingsMenu.activeSelf)
        {
            CloseSettings();
        }
        else
        {
            TogglePause();
        }
    }

    public void BackToMainMenu()
    {
        SceneLoader.Instance.MainMenu();
        TogglePause();
    }

    public void ToLevelSelect()
    {
        SceneLoader.Instance.LevelSelect();
        TogglePause();
    }
}
