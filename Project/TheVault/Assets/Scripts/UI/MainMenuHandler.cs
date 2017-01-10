using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : BaseMonoBehaviour
{
    private LevelData currentLevel;
    private int levelIndex = 0;

    [SerializeField]
    private Camera menuCamera;
    [SerializeField]
    private GameObject menuObj;

    [Header("Individual Level UI Elements")]
    [SerializeField]
    private Text levelTitleText;
    [SerializeField]
    private Text[] targetScoreTexts;

    private void Start()
    {
        GetLevel();
        InitializeLevelMenu();
    }

    private void GetLevel()
    {
        currentLevel = LevelManager.Instance.CurrentLevel;
    }

    private void InitializeLevelMenu()
    {
        levelTitleText.text = currentLevel.LevelTitle;

        //Show Target Scores
        float[] targetScores = currentLevel.StarRatingTargets;
        for (int i = 0; i < targetScoreTexts.Length; i++)
        {
            string targetScore = "$" + targetScores[i].ToString();
            targetScoreTexts[i].text = targetScore;
        }
    }

    public void OnClickLoadInLevel()
    {
        TurnOffMainMenu();
        InitialiseLevel();
    }

    private void TurnOffMainMenu()
    {
        menuCamera.enabled = false;
        menuObj.SetActive(false);
    }

    private void InitialiseLevel()
    {
        EventManager.TriggerEvent("SetupLevel");
    }

}
