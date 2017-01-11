using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimerHandler : BaseMonoBehaviour
{
    [SerializeField]
    private Text levelTimerText;
    private bool isTrackingTimer;

    private void OnEnable()
    {
        EventManager.StartListening("StartLevel", StartLevel);
        EventManager.StartListening("EndLevel", EndLevel);
    }

    private void OnDisable()
    {
        EventManager.StopListening("StartLevel", StartLevel);
        EventManager.StopListening("EndLevel", EndLevel);
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        UpdateLevelTimer();
    }

    void StartLevel()
    {
        isTrackingTimer = true;
    }

    void EndLevel()
    {
        isTrackingTimer = false;
        levelTimerText.color = Color.white;
    }

    public override void UpdateNormal()
    {
        if (isTrackingTimer)
            UpdateLevelTimer();
    }

    /// <summary>
    /// Triggered in Level Manager
    /// </summary>
    void UpdateLevelTimer()
    {
        float levelTimer = LevelManager.Instance.LevelTimer.timer;

        if (levelTimer < 10)
            levelTimerText.color = Color.red;

        string formattedTime = string.Empty;

        if(levelTimer > 0)
            formattedTime = ExtensionMethods.FormatTimeText(levelTimer);
        else
            formattedTime = ExtensionMethods.FormatTimeText(0);

        levelTimerText.text = formattedTime;
    }
}
