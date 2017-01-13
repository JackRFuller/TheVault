using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : BaseMonoBehaviour
{
    private bool isCursorDisabled;

    void OnEnable()
    {
        EventManager.StartListening("OpenVault", TurnOffCursor);

        EventManager.StartListening("EndLevel", TurnOnCursor);       
    }

    void OnDisable()
    {
        EventManager.StopListening("OpenVault", TurnOffCursor);

        EventManager.StopListening("EndLevel", TurnOnCursor);
        EventManager.StopListening("FailedLevel", TurnOnCursor);
    }

    public override void UpdateNormal()
    {
        if (isCursorDisabled)
            TurnOffCursor();
    }

    void TurnOffCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorDisabled = true;
    }

    void TurnOnCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorDisabled = false;
    }
}
