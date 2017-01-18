using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimerUIHandler : BaseMonoBehaviour
{
    [Header("UI ELements")]
    [SerializeField]
    private Text timerText;

    private bool isRunningTimer;

    private void OnEnable()
    {
        EventManager.StartListening("StartLevel", ToggleTimer);
    }

    private void OnDisable()
    {
        EventManager.StopListening("StartLevel", ToggleTimer);
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        float startTime = LevelManager.LevelTimer;
        string formattedTIme = ExtensionMethods.FormatTimeText(startTime);
        timerText.text = formattedTIme;
    }

    public override void UpdateNormal()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (!isRunningTimer)
            return;

        float startTime = LevelManager.LevelTimer;
        string formattedTIme = ExtensionMethods.FormatTimeText(startTime);
        timerText.text = formattedTIme;
    }

    void ToggleTimer()
    {
        if(isRunningTimer)
        {
            isRunningTimer = false;
        }
        else
        {
            isRunningTimer = true;
        }
    }
}
