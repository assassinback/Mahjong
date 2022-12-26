using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public int numberOfLevels;
    public string levelFileName;
    public string levelPatternFileName;
    void Awake()
    {
        _instance = this;
        PlayingFirstTime();
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
    public void LevelComplete()
    {
        try
        {
            TileManager._instance.currentLevelInfo = LevelManager._instance.levelCount[int.Parse(TileManager._instance.currentLevelInfo.levelName)];
            TileManager._instance.currentLevelPattern = LevelManager._instance.levelPattern[int.Parse(TileManager._instance.currentLevelInfo.levelName)-1];
            BoardManager._instance.table = TileManager._instance.currentLevelPattern;
            TileManager._instance.RetryLevel();
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
        
    }
    public void LevelFailed()
    {
        TileManager._instance.RetryLevel();
    }
    public void PlayingFirstTime()
    {
        if(PlayerPrefs.HasKey("PlayedFirstTime"))
        {
            return;
        }
        PlayerPrefs.SetInt("PlayedFirstTime", 1);
        SetUndo(0);
        SetTime(0);
        SetHints(0);

    }
    
}
