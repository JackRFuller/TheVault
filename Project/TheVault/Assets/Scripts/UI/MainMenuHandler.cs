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
    private AudioListener cameraAudioListener;
    [SerializeField]
    private GameObject menuObj;

    [Header("Top Level Menu Options")]
    [SerializeField]
    private GameObject levelSelectObj;
    [SerializeField]
    private GameObject instructionObj;

    [Header("Individual Level UI Elements")]
    [SerializeField]
    private Text levelTitleText;
    [SerializeField]
    private Text[] targetScoreTexts;

    private void OnEnable()
    {
        EventManager.StartListening("ResetLevel", Init);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ResetLevel", Init);
    }

    private void Start()
    {
        Init();
    }

    void Init()
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
        menuCamera.enabled = true;
        cameraAudioListener.enabled = true;

        levelTitleText.text = currentLevel.LevelTitle;

        //Show Target Scores
        float[] targetScores = currentLevel.StarRatingTargets;
        for (int i = 0; i < targetScoreTexts.Length; i++)
        {
            string targetScore = "$" + targetScores[i].ToString();
            targetScoreTexts[i].text = targetScore;
        }

        menuObj.SetActive(true);
    }

    public void OnClickLoadInLevel()
    {
        TurnOffMainMenu();
        InitialiseLevel();
    }

    private void TurnOffMainMenu()
    {
        menuCamera.enabled = false;
        cameraAudioListener.enabled = false;
        menuObj.SetActive(false);
        Debug.Log("Turn Off");
    }

    private void InitialiseLevel()
    {
        EventManager.TriggerEvent("SetupLevel");
    }

    public void OnClickToggleMenuMode()
    {
        if(levelSelectObj.activeInHierarchy)
        {
            levelSelectObj.SetActive(false);
            instructionObj.SetActive(true);
        }
        else
        {
            levelSelectObj.SetActive(true);
            instructionObj.SetActive(false);
        }
    }

}
