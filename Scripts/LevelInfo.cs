using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelInfo
{
    public string levelName="";
    public bool levelCompleted=false;
    public bool levelUnlocked=false;
    public int levelStars=0;
    public float levelTime=0;
    public int rows=0;
    public int columns = 0;
    public int layers = 0;
}
