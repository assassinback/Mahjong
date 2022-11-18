using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public int numberOfLevels;
    public string levelFileName;
    void Awake()
    {
        _instance = this;
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
    }
}
