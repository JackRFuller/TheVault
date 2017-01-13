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

    private void OnEnable()
    {
        EventManager.StartListening("OpenVault", Init);
        EventManager.StartListening("CollectedMoney", AddMoneyToBar);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OpenVault", Init);
        EventManager.StopListening("CollectedMoney", AddMoneyToBar);
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

        //Work Out Percentage
    }

    public override void UpdateNormal()
    {
        if (isLerping)
            LerpMoney();
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


}
