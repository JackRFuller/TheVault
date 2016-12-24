using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimerHandler : MonoBehaviour
{
    [SerializeField]
    private Text levelTimerText;

    private void OnEnable()
    {
        EventManager.StartListening("RunLevelTimer", UpdateLevelTimer);
    }

    private void OnDisable()
    {
        EventManager.StopListening("RunLevelTimer", UpdateLevelTimer);
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
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
