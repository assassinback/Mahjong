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
    public Table currentLevelPattern;
    public float originalTime;
    string levelFailText = "You Lose";
    string levelWinText = "You Win";
    //public List<int> ids;
    public List<GameObject> topLayerTiles;
    private void Awake()
    {
        _instance = this;
    }
    public void ClearCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void PlayLevel()
    {
        SoundManager._instance.PlayButtonClickSound();
        ClearCards();
        cards.Clear();
        selectTiles.Clear();
        UIManager._instance.AddToStack();
        currentLevelInfo = LevelManager._instance.levelCount[LevelManager._instance.levelCount.Count - 1];
        BoardManager._instance.table = LevelManager._instance.levelPattern[LevelManager._instance.levelPattern.Count - 1];
        BoardManager._instance.StartGenerating();
        LoadLevel();
        UIManager._instance.ResumeButtonClicked();
        UIManager._instance.SetLevelNameText("Level " + currentLevelInfo.levelName);
    }
    public void RetryLevel()
    {
        SoundManager._instance.PlayButtonClickSound();
        ClearCards();
        cards.Clear();
        selectTiles.Clear();
        UIManager._instance.AddToStack();
        BoardManager._instance.table = LevelManager._instance.levelPattern[int.Parse(currentLevelInfo.levelName)-1];
        BoardManager._instance.StartGenerating();
        LoadLevel();
        UIManager._instance.ResumeButtonClicked();
        UIManager._instance.SetLevelNameText("Level " + currentLevelInfo.levelName);
    }
    public void LoadLevel()
    {
        cards = new List<RectTransform>();
        selectTiles = new List<SelectTile>();
        cards.Clear();
        selectTiles.Clear();
        LoadTiles();
        originalTime = currentLevelInfo.levelTime;
        UIManager._instance.SetSliderMinMax(0, originalTime);
        topLayerTiles = BoardManager._instance.GetTopLayerTiles();
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
            UIManager._instance.nextLevelButton.onClick.RemoveAllListeners();
            UIManager._instance.nextLevelButton.onClick.AddListener(GameManager._instance.LevelFailed);
            GameManager._instance.Pause();
            
            if (GoogleAdsScript._instance.interstitial.IsLoaded())
            {
                GoogleAdsScript._instance.interstitial.Show();
            }
            GoogleAdsScript._instance.RequestInterstitial();
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
            UIManager._instance.nextLevelButton.onClick.RemoveAllListeners();
            UIManager._instance.nextLevelButton.onClick.AddListener(GameManager._instance.LevelComplete);
            LevelManager._instance.SaveLevelInfo();
            LevelManager._instance.GetPatterns();
            LevelManager._instance.GetLevelInfo();
            UIManager._instance.ShowLevelInfo();
            if (GoogleAdsScript._instance.interstitial.IsLoaded())
            {
                GoogleAdsScript._instance.interstitial.Show();
            }
            GameManager._instance.Pause();

            
        }
    }    
    public void AddTime()
    {
        SoundManager._instance.PlayButtonClickSound();
        if (GameManager._instance.GetTime() <= 0)
        {
            return;
        }
        originalTime += (currentLevelInfo.levelTime / 7);
        if (originalTime > currentLevelInfo.levelTime)
        {
            originalTime = currentLevelInfo.levelTime;
        }
        GameManager._instance.SetTime(GameManager._instance.GetTime()-1);
        UIManager._instance.SetLimitedValues();
    }
    public void UndoMove()
    {
        SoundManager._instance.PlayButtonClickSound();
        if (GameManager._instance.GetUndo() <= 0 || selectTiles.Count<=0)
        {
            return;
        }
        selectTiles.RemoveAt(selectTiles.Count-1);
        UIManager._instance.AddToStack();
        GameManager._instance.SetUndo(GameManager._instance.GetUndo() - 1);
        UIManager._instance.SetLimitedValues();
    }
    public int random(int min,int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public void GetHint()
    {
        SoundManager._instance.PlayButtonClickSound();
        if (GameManager._instance.GetHints()<=0)
        {
            return;
        }
        topLayerTiles = BoardManager._instance.GetTopLayerTiles();
        int j = 0;
        reselectid:
        int id = topLayerTiles[j].GetComponent<Tile>().id;
        if(!topLayerTiles[j].activeSelf)
        {
            j++;
            goto reselectid;
        }
        for(int i=0;i<topLayerTiles.Count;i++)
        {
            if(topLayerTiles[i].GetComponent<Tile>().id==id)
            {
                topLayerTiles[i].gameObject.SetActive(false);
                BoardManager._instance.RemoveEmptyRows();
                

            }
        }
        topLayerTiles = BoardManager._instance.GetTopLayerTiles();
        UIManager._instance.AddToStack();
        RefreshCards(id);
        GameManager._instance.SetHints(GameManager._instance.GetHints() - 1);
        UIManager._instance.SetLimitedValues();
    }
}
