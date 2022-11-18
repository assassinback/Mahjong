using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;
    public List<LevelInfo> levelCount;
    
    private void Awake()
    {
        _instance = this;
        
    }
    public void GetLevelInfo()
    {
        SaveSystem saveSystem = new SaveSystem();
        try
        {
            var x = ((Newtonsoft.Json.Linq.JArray)saveSystem.GetData(GameManager._instance.levelFileName)).ToObject<List<LevelInfo>>();
            levelCount = x;
        }
        catch (System.Exception)
        {

        }
        if (levelCount.Count == 0)
        {
            levelCount = new List<LevelInfo>();
            for (int i = 0; i < 50; i++)
            {
                LevelInfo levelInfo = new LevelInfo();
                levelInfo.levelCompleted = false;
                levelInfo.levelUnlocked = false;
                levelInfo.levelName = (i + 1).ToString();
                levelInfo.levelStars = 0;
                levelCount.Add(levelInfo);
            }
            saveSystem.SaveData(levelCount, GameManager._instance.levelFileName);

        }
    }
    public void SaveLevelInfo()
    {
        SaveSystem saveSystem = new SaveSystem();
        saveSystem.SaveData(levelCount, GameManager._instance.levelFileName);
        UIManager._instance.ShowLevelInfo();
    }
    private void Start()
    {
        GetLevelInfo();
        UIManager._instance.ShowLevelInfo();
    }
}
