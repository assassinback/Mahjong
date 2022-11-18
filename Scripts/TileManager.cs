using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class TileManager : MonoBehaviour
{
    public static TileManager _instance;
    public List<RectTransform> cards;
    public int selectedCount;
    public List<SelectTile> selectTiles;
    public LevelInfo currentLevelInfo;
    public float originalTime;
    string levelFailText = "You Lose";
    string levelWinText = "You Win";
    public List<int> ids;
    private void Awake()
    {
        _instance = this;
    }
    public void RetryLevel()
    {
        BoardManager._instance.StartGenerating();
        LoadLevel();
        UIManager._instance.ResumeButtonClicked();
    }
    public void LoadLevel()
    {
        cards = new List<RectTransform>();
        selectTiles = new List<SelectTile>();
        cards.Clear();
        selectTiles.Clear();
        LoadTiles();
        foreach(RectTransform rect in cards)
        {
            if(!ids.Contains(rect.GetComponent<Tile>().id))
            {
                ids.Add(rect.GetComponent<Tile>().id);
            }
        }
        originalTime = currentLevelInfo.levelTime;
        UIManager._instance.SetSliderMinMax(0, originalTime);
    }
    public void LoadTiles()
    {
        cards.AddRange(this.GetComponentsInChildren<RectTransform>());
        //Debug.Log("here");
        cards.Remove(this.GetComponent<RectTransform>());
        foreach (RectTransform card in cards)
        {

            card.gameObject.AddComponent<SelectTile>();
        }
    }
    public void RefreshSelectTiles(int id)
    {
        for (int k = 0; k < selectTiles.Count; k++)
        {
            if (id==selectTiles[k].id)
            {
                selectTiles.RemoveAt(k);
                k = -1;
            }
        }
        //Debug.Log("here");
        //selectTiles.RemoveAll(x => x== null);
    }
    public void RefreshCards(int id)
    {
        for (int k = 0; k < cards.Count; k++)
        {
            if (id == cards[k].GetComponent<Tile>().id)
            {
                cards.RemoveAt(k);
                k = -1;
            }
        }
        //cards = cards.Where(item => item != null).ToList();
    }
    private void Update()
    {
        originalTime -= Time.deltaTime;
        UIManager._instance.SetSliderValue();
        StartCoroutine(CheckLevelComplete());

    }
    private IEnumerator CheckLevelComplete()
    {
        yield return new WaitForSeconds(0.21f);
        if (originalTime <= 0 || selectTiles.Count >= 10)
        {
            UIManager._instance.ShowInGameUI();
            UIManager._instance.EnableLevelCompletePanel();
            UIManager._instance.SetCompleteLevelText(levelFailText);
            GameManager._instance.Pause();
        }
        else if (cards.Count <= 0)
        {
            UIManager._instance.ShowInGameUI();
            UIManager._instance.EnableLevelCompletePanel();
            UIManager._instance.SetCompleteLevelText(levelWinText);
            currentLevelInfo.levelCompleted = true;
            try
            {
                LevelManager._instance.levelCount[LevelManager._instance.levelCount.IndexOf(currentLevelInfo) + 1].levelUnlocked = true;
            }
            catch(System.Exception)
            {

            }
            
            int tempStars;
            if(currentLevelInfo.levelTime*0.66<originalTime)
            {
                tempStars = 3;
                currentLevelInfo.levelStars = 3;
            }
            else if(currentLevelInfo.levelTime * 0.33 < originalTime)
            {
                tempStars = 2;
                if(currentLevelInfo.levelStars<2)
                    currentLevelInfo.levelStars = 2;
            }
            else
            {
                tempStars = 1;
                if (currentLevelInfo.levelStars <= 1)
                    currentLevelInfo.levelStars = 1;
            }
            UIManager._instance.ShowStars(tempStars);
            LevelManager._instance.SaveLevelInfo();
            GameManager._instance.Pause();
            
        }
    }    
    public void AddTime()
    {
        originalTime += (currentLevelInfo.levelTime / 7);
        GameManager._instance.SetTime(GameManager._instance.GetTime()-1);
    }
    public void UndoMove()
    {
        selectTiles.RemoveAt(selectTiles.Count-1);
        GameManager._instance.SetUndo(GameManager._instance.GetUndo() - 1);
    }
    public int random(int min,int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public void GetHint()
    {
        int i = random(0, ids.Count);
        for (int j=0;j<ids.Count;j++)
        {
            if(i==j)
            {
                for(int k=0;k<cards.Count;k++)
                {

                }
            }
        }
        GameManager._instance.SetHints(GameManager._instance.GetHints() - 1);
    }
}
