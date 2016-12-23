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
            formattedTime = ExtensionMehtods.FormatTimeText(levelTimer);
        else
            formattedTime = ExtensionMehtods.FormatTimeText(0);

        levelTimerText.text = formattedTime;
    }
}
