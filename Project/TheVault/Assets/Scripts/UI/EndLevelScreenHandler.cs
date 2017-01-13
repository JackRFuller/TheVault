using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelScreenHandler : BaseMonoBehaviour
{
    [Header("Gameplay Objects")]
    [SerializeField]
    private GameObject endScreenObj;
    [SerializeField]
    private GameObject totalStolenCashObj;
    [SerializeField]
    private GameObject timeRemainingObj;
    [SerializeField]
    private GameObject stolenValuableObj;
    [SerializeField]
    private GameObject[] starObjs;
    [SerializeField]
    private GameObject restartButtonObj;
    [SerializeField]
    private GameObject lobbyButtonObj;

    [Header("UI Elements")]
    [SerializeField]
    private Text totalCashAmountText;
    [SerializeField]
    private Text timeRemainingText;
    [SerializeField]
    private Image greenTickImage;
    [SerializeField]
    private Image redCrossImage;
    [SerializeField]
    private Image[] starImages;

    //Level Manager Values
    private float totalCollectedCash;
    private float timeRemaining;
    private bool hasCollectedValuable;
    private float[] starTargets;

    //Collected Cash lerp Variables
    private bool isCountingUpCash;
    private float timeStartedCash;
    private float startingCashValue = 0;
    private float targetCashValue;
    private float cashCountUpSpeed = 1;

    private void OnEnable()
    {
        EventManager.StartListening("ExittedLevel", RevealEndScreen);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ExittedLevel", RevealEndScreen);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        endScreenObj.SetActive(false);
        totalStolenCashObj.SetActive(false);
        timeRemainingObj.SetActive(false);
        stolenValuableObj.SetActive(false);

        for (int i = 0; i < starObjs.Length; i++)
        {
            starObjs[i].SetActive(false);
        }

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled = false;
        }

        restartButtonObj.SetActive(false);
        lobbyButtonObj.SetActive(false);
    }

    void RevealEndScreen()
    {
        totalCollectedCash = LevelManager.MoneyCollected;
        timeRemaining = LevelManager.LevelTimer;
        hasCollectedValuable = LevelManager.Instance.HasCollectedValuable;
        starTargets = LevelManager.Instance.CurrentLevel.StarRatingTargets;

        StartCoroutine(EndScreenProcess());        
    }

    IEnumerator EndScreenProcess()
    {
        endScreenObj.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        totalStolenCashObj.SetActive(true);
        targetCashValue = totalCollectedCash;
        timeStartedCash = Time.time;
        isCountingUpCash = true;
        yield return new WaitForSeconds(0.2f);

        timeRemainingObj.SetActive(true);
        string formattedTimeRemaining = ExtensionMethods.FormatTimeText(timeRemaining);
        timeRemainingText.text = formattedTimeRemaining;
        yield return new WaitForSeconds(0.2f);

        stolenValuableObj.SetActive(true);
        if(hasCollectedValuable)
        {
            greenTickImage.enabled = true;
            redCrossImage.enabled = false;
        }
        else
        {
            greenTickImage.enabled = false;
            redCrossImage.enabled = true;
        }

        yield return new WaitForSeconds(0.2f);

        for(int i = 0; i < starObjs.Length; i++)
        {
            starObjs[i].SetActive(true);
        }

        for(int i = 0; i < starTargets.Length; i++)
        {
            yield return new WaitForSeconds(0.3f);
            if(totalCollectedCash >= starTargets[i])
            {
                starImages[i].enabled = true;
            }
        }

        yield return new WaitForSeconds(0.2f);
        //restartButtonObj.SetActive(true);
        lobbyButtonObj.SetActive(true);
    }

   

    public override void UpdateNormal()
    {
        if(isCountingUpCash)
        {
            CountUpCash();
        }       
    } 

    private void CountUpCash()
    {
        float timeSinceStarted = Time.time - timeStartedCash;
        float percentageComplete = timeSinceStarted / cashCountUpSpeed;

        float cash = Mathf.Lerp(startingCashValue, targetCashValue, percentageComplete);

        string cashString = string.Empty;
        cashString = "$" + cash.ToString("F0");
        totalCashAmountText.text = cashString;


        if(percentageComplete >= 1.0f)
        {
            isCountingUpCash = false;
        }
    }   

    public void OnClickRestart()
    {
        Debug.Log("Restart");
    }

    public void OnClickReturnToLobby()
    {
        Init();
        EventManager.TriggerEvent("ResetLevel");
    }


}

