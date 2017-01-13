using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowDownTimeUIHandler : BaseMonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField]
    private GameObject slowDownTimeBarObj;

    [Header("UI Elements")]
    [SerializeField]
    private Image slowDownBarImage;

    private float maxSloMoAvailable;

    private void OnEnable()
    {
        EventManager.StartListening("SetupSlowMo", SetupSlowMoBar);
        EventManager.StartListening("UsingSlowMotion", ShowBar);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SetupSlowMo", SetupSlowMoBar);
        EventManager.StopListening("UsingSlowMotion", ShowBar);
    }

    private void Start()
    {
        TurnOffBar();
    }

    void SetupSlowMoBar()
    {       
        maxSloMoAvailable = SlowDownTimerHandler.SlowDownAvailable;
    }

    void ShowBar()
    {
        StopAllCoroutines();

        if(!slowDownTimeBarObj.activeInHierarchy)
            slowDownTimeBarObj.SetActive(true);

        float fillAmount = SlowDownTimerHandler.SlowDownAvailable / maxSloMoAvailable;
        slowDownBarImage.fillAmount = fillAmount;

        StartCoroutine(TurnOffSlowMoBar());        
    }

    private IEnumerator TurnOffSlowMoBar()
    {
        yield return new WaitForSeconds(2.5f);
        TurnOffBar();
    }

    private void TurnOffBar()
    {
        slowDownTimeBarObj.SetActive(false);
    }

}
