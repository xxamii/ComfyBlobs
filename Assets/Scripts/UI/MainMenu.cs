using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainMenuMusic;

    public void Start()
    {
        if (mainMenuMusic != null)
            AudioPlayer.Instance.PlayMusic(mainMenuMusic);
    }

    public void Play()
    {
        SceneLoader.Instance.LevelSelect();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
} 
