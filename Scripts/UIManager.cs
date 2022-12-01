using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
[System.Serializable]
public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public GameObject cloudPrefab;
    public int cloudCounter;
    public GameObject homeScreenPanel;
    public GameObject levelSelectPanel;
    public GameObject luckyWheelPanel;
    public GameObject limitedButtonPanel;
    public GameObject scrollViewLevels;
    public GameObject levelCompleteButton;
    public GameObject levelLockButton;
    public Sprite filledStar;
    public Sprite emptyStar;
    public TextMeshProUGUI hint;
    public TextMeshProUGUI undo;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI hintWheel;
    public TextMeshProUGUI undoWheel;
    public TextMeshProUGUI timerWheel;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI levelNameText;
    public TextMeshProUGUI levelNameTextLevelCompleteLabel;
    public GameObject gameCanvas;
    public GameObject UICanvas;
    public GameObject inGameUI;
    public GameObject pausePanel;
    public GameObject levelCompletePanel;
    public Slider timeSlider;
    public Sprite FilledStarLevelComplete;
    public Sprite EmptyStarLevelFail;
    public GameObject[] stars;
    public GameObject scrollViewCardSelected;
    public GameObject selectedCardImage;
    public Button nextLevelButton;
    public GameObject settingCanvas;
    [field: SerializeField] public List<StackTileData> data = new List<StackTileData>();
    private void Start()
    {
        SetLimitedValues();
        ShowUICanvas();
        EnableHomeScreen();

        StartCoroutine(makeClouds());
    }
    void Awake()
    {
        _instance = this;

    }

    IEnumerator makeClouds()
    {


        while (true)
        {


            GameObject cloud = Instantiate(cloudPrefab, new Vector3(), Quaternion.identity, UICanvas.transform);
            cloud.GetComponent<RectTransform>().anchoredPosition = new Vector2(cloud.GetComponent<RectTransform>().anchoredPosition.x, UnityEngine.Random.Range(180, 750));
            cloud.transform.SetSiblingIndex(1);
            try
            {
                GameObject temp = GameObject.FindGameObjectWithTag("HomeUI").gameObject;
            }
            catch (System.Exception)
            {
                break;
            }
            int randomWait = UnityEngine.Random.Range(8, 12);
            yield return new WaitForSeconds(randomWait);
        }
    }
    public void SetLevelNameText(string text)
    {
        levelNameText.text = text;
    }
    public void DisableAllHomeUI()
    {
        homeScreenPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        luckyWheelPanel.SetActive(false);
        limitedButtonPanel.SetActive(false);
        pausePanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        ResumeClicked();
    }
    public void EnableLevelCompletePanel()
    {
        DisableAllHomeUI();
        levelCompletePanel.SetActive(true);
    }
    public void EnablePausePanel()
    {
        SoundManager._instance.PlayButtonClickSound();
        DisableAllHomeUI();
        ShowInGameUI();
        pausePanel.SetActive(true);
        PauseClicked();
    }
    public void EnableHomeScreen()
    {
        SoundManager._instance.PlayButtonClickSound();
        DisableAllHomeUI();
        homeScreenPanel.SetActive(true);
    }
    public void EnableLevelSelectScreen()
    {
        SoundManager._instance.PlayButtonClickSound();
        DisableAllHomeUI();
        levelSelectPanel.SetActive(true);
    }
    public void EnableLuckyWheelScreen()
    {
        SoundManager._instance.PlayButtonClickSound();
        DisableAllHomeUI();
        luckyWheelPanel.SetActive(true);
        limitedButtonPanel.SetActive(true);
    }
    public void EnableLimitedButtonsScreen()
    {
        DisableAllHomeUI();
        limitedButtonPanel.SetActive(true);
    }
    public void ShowLevelInfo()
    {
        foreach (Transform child in scrollViewLevels.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < GameManager._instance.numberOfLevels; i++)
        {
            if (LevelManager._instance.levelCount[i].levelUnlocked)
            {
                GameObject btn = Instantiate(levelCompleteButton, scrollViewLevels.transform);
                btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = LevelManager._instance.levelCount[i].levelName;
                if (LevelManager._instance.levelCount[i].levelCompleted)
                {
                    for (int j = 1; j <= LevelManager._instance.levelCount[i].levelStars; j++)
                    {
                        btn.transform.GetChild(j).GetComponent<Image>().sprite = filledStar;
                    }


                }
                btn.GetComponent<LevelStartButton>().levelInfo = LevelManager._instance.levelCount[i];
                btn.GetComponent<LevelStartButton>().patternInfo = LevelManager._instance.levelPattern[i];
            }
            else
            {
                GameObject btn = Instantiate(levelLockButton, scrollViewLevels.transform);
            }

        }
    }
    public void ShowUICanvas()
    {
        DisableAllCanvas();
        UICanvas.SetActive(true);
    }
    public void ShowGameCanvas()
    {
        DisableAllCanvas();
        gameCanvas.SetActive(true);
    }
    public void ShowInGameUI()
    {
        DisableAllCanvas();
        ShowGameCanvas();
        inGameUI.SetActive(true);
    }
    public void DisableAllCanvas()
    {
        gameCanvas.SetActive(false);
        UICanvas.SetActive(false);
        inGameUI.SetActive(false);
        settingCanvas.SetActive(false);
        ResumeClicked();
    }
    public void EnableSettingsCanvas()
    {
        settingCanvas.SetActive(true);
    }
    public void DisableSettingsCanvas()
    {
        settingCanvas.SetActive(false);
    }
    public void GotoLevelSelectPanel()
    {
        SoundManager._instance.PlayButtonClickSound();
        TileManager._instance.ClearCards();
        DisableAllHomeUI();
        DisableAllCanvas();
        ShowUICanvas();
        EnableLevelSelectScreen();
    }
    public void SetLimitedValues()
    {
        hint.text = PlayerPrefs.GetInt("Hint") + "";
        undo.text = PlayerPrefs.GetInt("Undo") + "";
        timer.text = PlayerPrefs.GetInt("Timer") + "";
        hintWheel.text = PlayerPrefs.GetInt("Hint") + "";
        undoWheel.text = PlayerPrefs.GetInt("Undo") + "";
        timerWheel.text = PlayerPrefs.GetInt("Timer") + "";
    }
    public void ResumeButtonClicked()
    {
        SoundManager._instance.PlayButtonClickSound();
        DisableAllHomeUI();
        DisableAllCanvas();
        ShowGameCanvas();
    }
    public void ResumeClicked()
    {
        GameManager._instance.Unpause();
    }
    public void PauseClicked()
    {
        GameManager._instance.Pause();
    }
    public void SetCompleteLevelText(string text)
    {
        levelCompleteText.text = text;
        levelNameTextLevelCompleteLabel.text = text;
    }
    public void SetSliderValue()
    {
        timeSlider.value = TileManager._instance.originalTime;
    }
    public void SetSliderMinMax(float minVal, float maxVal)
    {
        timeSlider.minValue = minVal;
        timeSlider.maxValue = maxVal;
    }
    public void ShowStars(int wonStars)
    {
        for(int i=0;i<stars.Length; i++)
        {
            if(wonStars>i)
            {
                stars[i].GetComponent<Image>().sprite = FilledStarLevelComplete;
            }
            else
            {
                stars[i].GetComponent<Image>().sprite = EmptyStarLevelFail;
            }
            
        }
    }
    public void AddToStack()
    {
        CreateStack();
        //SortStack();
    }
    public List<SelectTile> SortStack(List<SelectTile> data1)
    {

        data1 = data1.OrderBy(go => go.id).ToList();
        

        return data1;
    }
    public void CreateStack()
    {
        for (int i = 0; i < scrollViewCardSelected.transform.childCount; i++)
        {
            scrollViewCardSelected.transform.GetChild(i).gameObject.SetActive(false);
        }
        List<SelectTile> selectTiles = TileManager._instance.selectTiles;
        selectTiles = SortStack(selectTiles);
        for (int i=0;i< selectTiles.Count; i++)
        {
            scrollViewCardSelected.transform.GetChild(i).gameObject.SetActive(true);
            scrollViewCardSelected.transform.GetChild(i).gameObject.GetComponent<Image>().sprite=selectTiles[i].gameObject.GetComponent<Image>().sprite;
        }
    }
}
