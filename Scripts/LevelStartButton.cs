using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelStartButton : MonoBehaviour
{
    public LevelInfo levelInfo;
    public Table patternInfo;
    public Button startLevelButton;
    
    private void Start()
    {
        startLevelButton = GetComponent<Button>();
        startLevelButton.onClick.AddListener(StartLevel);
    }
    private void StartLevel()
    {
        TileManager._instance.currentLevelInfo = levelInfo;
        TileManager._instance.currentLevelPattern = patternInfo;
        UIManager._instance.SetLevelNameText("Level "+levelInfo.levelName);
        //BoardManager._instance.rows = levelInfo.rows;
        //BoardManager._instance.columns = levelInfo.columns;
        //BoardManager._instance.layers = levelInfo.layers;
        BoardManager._instance.table = patternInfo;
        BoardManager._instance.StartGenerating();
        UIManager._instance.ShowGameCanvas();
        TileManager._instance.LoadLevel();
        UIManager._instance.FirstTimeLevel();
    }
}
