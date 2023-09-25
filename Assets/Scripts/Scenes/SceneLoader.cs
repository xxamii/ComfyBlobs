using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : DontDestroyOnLoadSingleton<SceneLoader>
{

    public static int FINAL_LEVEL = 5;
    
    private SceneFader _fader;
    
    private string _currentScene;
    private string _sceneToLoad;
    private bool _canLoad = true;

    private void Start()
    {
        _fader = GetComponent<SceneFader>();
        _currentScene = SceneManager.GetActiveScene().name;
    }

    public void Restart()
    {
        QueueLoadScene(_currentScene);
    }

    public void QueueLoadScene(string scene)
    {
        if (_canLoad)
        {
            _sceneToLoad = scene;
            _fader.FadeOut();
            _canLoad = false;
        }
    }
    
    // Called from animation
    public void OnFadeOutComplete()
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);

        if (operation != null)
        {
            _canLoad = false;
            operation.completed += OnLoadSceneComplete;
        }
        else
        {
            Debug.LogError("Could not load scene " + _sceneToLoad);
        }
    }

    private void OnLoadSceneComplete(AsyncOperation operation)
    {
        _canLoad = true;
        _currentScene = _sceneToLoad;
        _sceneToLoad = null;
        _fader.FadeIn();
    }

    public void MainMenu()
    {
        QueueLoadScene("MainMenu");
    }

    public void LevelSelect()
    {
        QueueLoadScene("LevelSelect");
    }

    public void LoadLevel(int levelNumber)
    {
        
        if (levelNumber > FINAL_LEVEL)
        {
            LevelSelect();
        }
        else
        {
            string levelScene = "Level" + levelNumber;
            QueueLoadScene(levelScene);            
        }
    }
}
