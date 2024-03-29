using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevelPatterns : MonoBehaviour
{
    public static GenerateLevelPatterns _instance;
    public List<GameObject> tiles;
    private void Awake()
    {
        _instance = this;
    }
    private void MakeColumns(ref Column[] columns)
    {
        for(int i=0;i<columns.Length;i++)
        {
            MakeRows(ref columns[i].rows);
        }
    }
    private void MakeRows(ref Row[] rows)
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = new Row();
        }
    }
    public List<Table> GenerarePatterns()
    {

        List<Table> levelInfo = new List<Table>();
        int i = 1;
        Layer[] levelPattern;
        //Level 1 Start
        levelPattern = new Layer[2];
        
        levelPattern[0] = new Layer(3,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(2, 2);
        MakeColumns(ref levelPattern[1].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 2 Start
        levelPattern = new Layer[2];
        
        levelPattern[0] = new Layer(2,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4,3);
        MakeColumns(ref levelPattern[1].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 3 Start
        levelPattern = new Layer[3];
        levelPattern[0] = new Layer(4,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 3);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(2,2);
        MakeColumns(ref levelPattern[2].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 4 Start
        levelPattern = new Layer[3];
        levelPattern[0] = new Layer(5,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(2,2);
        MakeColumns(ref levelPattern[2].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 5 Start
        levelPattern = new Layer[3];
        levelPattern[0] = new Layer(3,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(2, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4,2);
        MakeColumns(ref levelPattern[2].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 6 Start
        levelPattern = new Layer[3];
        levelPattern[0] = new Layer(5,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(6,6);
        MakeColumns(ref levelPattern[2].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 7 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(4,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(3,4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(2, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2,2);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 8 Start
        levelPattern = new Layer[3];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(5, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 9 Start
        levelPattern = new Layer[3];
        levelPattern[0] = new Layer(6, 6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 6);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(6, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 10 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(4,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 6);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2,2);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 11 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 2);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(2,6);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(3,4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 12 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 3);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(3, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 13 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(4, 6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(5, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 14 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 6);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 5);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 15 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 2);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(5, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 3);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 16 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 6);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(6,2);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 17 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 2);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 18 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 3);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(3, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2,2);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 19 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(5, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 20 Start
        levelPattern = new Layer[4];
        levelPattern[0] = new Layer(6, 6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 6);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 5);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 21 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(4,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(2, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 2);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(2,2);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 22 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(4, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 2);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 3);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(2, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 23 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(4,5);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 3);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 2);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(2,4);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 24 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(5,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(3, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 3);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(2,2);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 25 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(4, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6, 2);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 2);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4,4);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(2, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 26 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(5, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 2);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 27 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(4,6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(5, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4,4);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 28 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(5, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 6);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(6, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 29 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(6, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(5, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 6);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(6, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 30 Start
        levelPattern = new Layer[5];
        levelPattern[0] = new Layer(6,6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6,4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 5);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(5,4);
        MakeColumns(ref levelPattern[4].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 31 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(3,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 3);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(2, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(2,2);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(2,2);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 32 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(4,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4,3);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 2);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(2,4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(2,2);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 33 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(2, 2);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(3, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 3);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(2,2);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 34 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(4,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4,3);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(2,2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(3, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(2,2);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 35 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(4,3);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(6, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(2,2);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 36 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(6,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 2);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 6);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(6,4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(2,4);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 37 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(5, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(2, 2);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(3, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 2);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(6, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(6, 2);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 38 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(5, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(6, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(5, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(6, 2);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 39 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(6,4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6,2);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4,6);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(6, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4,5);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(4, 4);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 40 Start
        levelPattern = new Layer[6];
        levelPattern[0] = new Layer(6,6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6,4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(6,4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(5,4);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4,5);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(4,4);
        MakeColumns(ref levelPattern[5].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 41 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(2, 2);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(2, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 2);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(3, 4);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(2, 2);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(4, 3);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(2, 2);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 42 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(4,5);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4,4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(3, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4,3);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4, 3);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(3, 4);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(2,2);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 43 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(4, 6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 5);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(5, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 5);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(6, 2);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(2, 6);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(2,2);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 44 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(4, 4);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(5,4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 5);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4, 2);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(2, 2);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(2, 4);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 45 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(4, 6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6,4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(5, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(6, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(4, 5);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(4, 4);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 46 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(6,6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(5, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(6, 2);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(4,4);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(4,3);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 47 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(6,6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6, 4);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(4, 6);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(4,4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(4,4);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(4, 3);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 48 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(2, 2);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(4, 2);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(2, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 3);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(3, 4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(4, 4);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(3,4);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 49 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(6,6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6, 6);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(6, 4);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(4, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(5,4);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(4,4);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(4, 4);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        i++;
        //Level 50 Start
        levelPattern = new Layer[7];
        levelPattern[0] = new Layer(6, 6);
        MakeColumns(ref levelPattern[0].columns);
        levelPattern[1] = new Layer(6, 6);
        MakeColumns(ref levelPattern[1].columns);
        levelPattern[2] = new Layer(6, 6);
        MakeColumns(ref levelPattern[2].columns);
        levelPattern[3] = new Layer(6, 6);
        MakeColumns(ref levelPattern[3].columns);
        levelPattern[4] = new Layer(6, 6);
        MakeColumns(ref levelPattern[4].columns);
        levelPattern[5] = new Layer(6, 6);
        MakeColumns(ref levelPattern[5].columns);
        levelPattern[6] = new Layer(6, 6);
        MakeColumns(ref levelPattern[6].columns);
        levelInfo.Add(new Table(levelPattern));
        levelInfo[i-1].levelName = i.ToString();
        print(levelInfo.Count);
        //return
        return levelInfo;
    }
}
