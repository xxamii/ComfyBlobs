using System;
using System.Collections;
using System.Security.AccessControl;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    public int NextLevel => levelNumber + 1;

    private PlayerInput _input;
    private CharacterMovement _movement;
    private AudioPlayer _audioPlayer;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
        _movement = FindObjectOfType<CharacterMovement>();
        _audioPlayer = AudioPlayer.Instance;
        _sceneLoader = SceneLoader.Instance;
    }

    public void LoadNextLevel()
    {
        _input.CanInput = false;
        _movement.CanMove = false;
        _audioPlayer.PlayEndLevel();
        StartCoroutine(LoadNextLevelRoutine(4f));
    }
    
    private IEnumerator LoadNextLevelRoutine(float t)
    {
        yield return new WaitForSeconds(t);
        _sceneLoader.LoadLevel(NextLevel);
    }
}