using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    
    public void Level(int N)
    {
        SceneLoader.Instance.QueueLoadScene("Level" + N);
    }

    public void MainMenu()
    {
        SceneLoader.Instance.QueueLoadScene("MainMenu");
    }
}
