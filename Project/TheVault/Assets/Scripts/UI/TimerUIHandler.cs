using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIHandler : BaseMonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField]
    private GameObject timerObj;

    [Header("UI Elements")]
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Image timeIconImage;

    private bool isRunningTimer;

    private void OnEnable()
    {
        EventManager.StartListening("OpenVault", TurnOnTimer);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OpenVault", TurnOnTimer);
    }

    private void Start()
    {
        TurnOffTimer();
    }

    private void TurnOffTimer()
    {
        timerObj.SetActive(false);
        isRunningTimer = false;
    }

    private void TurnOnTimer()
    {
        timeText.color = Color.white;
        timeIconImage.color = Color.white;

        timerObj.SetActive(true);
        isRunningTimer = true;
    }

    public override void UpdateNormal()
    {
        if (isRunningTimer)
        {
            RunTimer();
        }
    }

    private void RunTimer()
    {
        string formattedTime = ExtensionMethods.FormatTimeText(LevelManager.LevelTimer);
        timeText.text = formattedTime;

        if (LevelManager.LevelTimer <= 10)
        {
            if (timeText.color != Color.red)
            {
                timeText.color = Color.red;
                timeIconImage.color = Color.red;
            }
        }
    }
}
