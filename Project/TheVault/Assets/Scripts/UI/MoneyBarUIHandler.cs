using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBarUIHandler : BaseMonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField]
    private GameObject moneyBarObjs;

    [Header("UI Elements")]
    [SerializeField]
    private Text amountOfMoneyText;
    [SerializeField]
    private Image[] starImages;
    [SerializeField]
    private Image moneyBar;

    private float[] starTargets;
    private int numberOfStarsCollected = 0;

    //Money Lerp Variables
    private const float lerpSpeed = 0.5f;
    private float timeStarted;
    private float startValue;
    private float targetValue;
    private bool isLerping;
    private float currentMoneyValue;

    //Money Bar Lerp Variables
    private const float barLerpSpeed = 0.1f;
    private float barTimeStarted;
    private float barStartValue;
    private float barTargetValue;
    private bool isBarLerping;

    private void OnEnable()
    {
        EventManager.StartListening("OpenVault", Init); //Turns On Bar
        EventManager.StartListening("CollectedMoney", AddMoneyToBar);
        EventManager.StartListening("EndLevel", TurnOffBar); //Turns Off Bar
    }

    private void OnDisable()
    {
        EventManager.StopListening("OpenVault", Init);
        EventManager.StopListening("CollectedMoney", AddMoneyToBar);
        EventManager.StopListening("EndLevel", TurnOffBar); //Turns Off Bar
    }

    private void Start()
    {
        TurnOffBar();
    }

    void Init()
    {
        starTargets = LevelManager.Instance.CurrentLevel.StarRatingTargets;

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled = false;
        }

        moneyBar.fillAmount = 0;
        numberOfStarsCollected = 0;

        string money = "$0";
        amountOfMoneyText.text = money;

        TurnOnBar();
    }


    private void TurnOffBar()
    {
        moneyBarObjs.SetActive(false);
        isBarLerping = false;
        isLerping = false;
        currentMoneyValue = 0;
    }

    private void TurnOnBar()
    {
        moneyBarObjs.SetActive(true);        
    }

   
    void AddMoneyToBar()
    {
        //Setup Lerp
        startValue = currentMoneyValue;
        targetValue = LevelManager.MoneyCollected;
        timeStarted = Time.time;
        isLerping = true;

        //Setup Bar Lerp
        barStartValue = moneyBar.fillAmount;
        //Work Out Percentage
        if (numberOfStarsCollected == 0)
        {
            barTargetValue = LevelManager.MoneyCollected / starTargets[numberOfStarsCollected];
        }
        else
        {
            float difference = starTargets[numberOfStarsCollected] - starTargets[numberOfStarsCollected - 1];
            float amountTowardsStar = LevelManager.MoneyCollected - starTargets[numberOfStarsCollected - 1];
            barTargetValue = amountTowardsStar / difference;
        }
        barTimeStarted = Time.time;
        isBarLerping = true;
    }

    public override void UpdateNormal()
    {
        if (isLerping)
            LerpMoney();

        if (isBarLerping)
            LerpBar();
    }

    void LerpMoney()
    {
        float timeSinceStarted = Time.time - timeStarted;
        float percentageComplete = timeSinceStarted / lerpSpeed;

        float moneyValue = Mathf.Lerp(startValue, targetValue, percentageComplete);
        currentMoneyValue = moneyValue;

        string money = "$" + moneyValue.ToString("F0");
        amountOfMoneyText.text = money;

        if(percentageComplete >= 1.0f)
        {
            isLerping = false;
        }
    }

    void LerpBar()
    {
        float timeSinceStarted = Time.time - barTimeStarted;
        float percentageComplete = timeSinceStarted / barLerpSpeed;

        float fillAmount = Mathf.Lerp(barStartValue, barTargetValue, percentageComplete);
        moneyBar.fillAmount = fillAmount;

        if(percentageComplete >= 1.0f)
        {
            if(moneyBar.fillAmount >= 1.0f)
            {
                TurnOnStars(numberOfStarsCollected);
                numberOfStarsCollected++;
                moneyBar.fillAmount = 0;
                
            }

            isBarLerping = false;
        }
    }

    void TurnOnStars(int starIndex)
    {
        starImages[starIndex].enabled = true;
    }


}
