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
    public void SetHints(int amount)
    {
        PlayerPrefs.SetInt("Hint",amount);
    }
    public void SetTime(int amount)
    {
        PlayerPrefs.SetInt("Timer", amount);
    }
    public void SetUndo(int amount)
    {
        PlayerPrefs.SetInt("Undo", amount);
    }
    public int GetHints()
    {
        return PlayerPrefs.GetInt("Hint");
    }
    public int GetTime()
    {
        return PlayerPrefs.GetInt("Timer");
    }
    public int GetUndo()
    {
        return PlayerPrefs.GetInt("Undo");
    }
}
