using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailureScreenHandler : BaseMonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField]
    private GameObject failureScreenObj;

    [Header("Second Tier Objects")]
    [SerializeField]
    private GameObject initialScreenObj;
    [SerializeField]
    private GameObject secondaryScreenObj;

    [Header("UI Elements")]
    [SerializeField]
    private Image rebootBarImage;

    //Lerping Variables
    private float timeStarted;
    private bool isFillingUpBar;
    private const float fillSpeed = 2;

    private void OnEnable()
    {
        EventManager.StartListening("FailedLevel", TurnOnFailureScreen);
    }

    private void OnDisable()
    {
        EventManager.StopListening("FailedLevel", TurnOnFailureScreen);
    }

    private void Start()
    {
        TurnOffFailureScreen();
    }

    void TurnOnFailureScreen()
    {
        failureScreenObj.SetActive(true);
        initialScreenObj.SetActive(true);
        secondaryScreenObj.SetActive(false);
    }

    void TurnOffFailureScreen()
    {
        failureScreenObj.SetActive(false);
        initialScreenObj.SetActive(false);
        secondaryScreenObj.SetActive(false);

        rebootBarImage.fillAmount = 0;
    }

    void StartRebootingProcess()
    {
        initialScreenObj.SetActive(false);
        secondaryScreenObj.SetActive(true);
        
        timeStarted = Time.time;
        isFillingUpBar = true;
    }

    public override void UpdateNormal()
    {
        if (isFillingUpBar)
            FillUpRebootBar();
    }

    void FillUpRebootBar()
    {
        float timeSinceStarted = Time.time - timeStarted;
        float percentageComplete = timeSinceStarted / fillSpeed;

        float fillAmount = Mathf.Lerp(0, 1, percentageComplete);

        rebootBarImage.fillAmount = fillAmount;

        if(percentageComplete >= 1.0f)
        {
            TurnOffFailureScreen();
        }
    }

    public void OnClickReboot()
    {
        StartRebootingProcess();
    }

}
