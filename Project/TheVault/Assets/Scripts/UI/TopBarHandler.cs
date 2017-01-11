using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarHandler : BaseMonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject blurryBakcgroundObj;
    [SerializeField]
    private GameObject levelTimerObj;
    [SerializeField]
    private GameObject cashBarObj;

    private void OnEnable()
    {
        EventManager.StartListening("OpenVault", TurnOnTopBar);
        EventManager.StartListening("ExittedLevel", TurnOffTopBar);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OpenVault", TurnOnTopBar);
        EventManager.StopListening("ExittedLevel", TurnOffTopBar);
    }

    private void Start()
    {
        TurnOffTopBar();
    }

    private void TurnOffTopBar()
    {
        blurryBakcgroundObj.SetActive(false);
        levelTimerObj.SetActive(false);
        cashBarObj.SetActive(false);
    }

    private void TurnOnTopBar()
    {
        blurryBakcgroundObj.SetActive(true);
        levelTimerObj.SetActive(true);
        cashBarObj.SetActive(true);
    }
}
