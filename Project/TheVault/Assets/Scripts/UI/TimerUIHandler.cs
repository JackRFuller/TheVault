using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIHandler : BaseMonoBehaviour
{
    //Components
    private AudioSource timerAudio;

    [Header("UI Objects")]
    [SerializeField]
    private GameObject timerObj;

    [Header("UI Elements")]
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Image timeIconImage;

    private bool isRunningTimer;
    private bool isPlayingSiren;
    private int sirenCount = 10;

    private void OnEnable()
    {
        EventManager.StartListening("OpenVault", TurnOnTimer); //Turns on Bar
        EventManager.StartListening("EndLevel", TurnOffTimer); //Turns off Bar
    }

    private void OnDisable()
    {
        EventManager.StopListening("OpenVault", TurnOnTimer);
        EventManager.StopListening("EndLevel", TurnOffTimer); //Turns off Bar
    }

    private void Start()
    {
        timerAudio = this.GetComponent<AudioSource>();

        TurnOffTimer();
    }

    private void TurnOffTimer()
    {
        timerObj.SetActive(false);
        isRunningTimer = false;

        isPlayingSiren = false;
        sirenCount = 0;
        timerAudio.Stop();
    }

    private void TurnOnTimer()
    {
        timeText.color = Color.white;
        timeIconImage.color = Color.white;

        timerObj.SetActive(true);
        isRunningTimer = true;
        sirenCount = 10;
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
            if (!isPlayingSiren)
                StartCoroutine(PlaySiren());

            if (timeText.color != Color.red)
            {
                timeText.color = Color.red;
                timeIconImage.color = Color.red;
            }
        }
    }

    private IEnumerator PlaySiren()
    {
        while(sirenCount > 0)
        {
            isPlayingSiren = true;
            timerAudio.Play();
            sirenCount--;
            yield return new WaitForSeconds(0.9f);
        }
        
    }
}
