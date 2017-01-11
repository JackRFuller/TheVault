using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : BaseMonoBehaviour
{
    void OnEnable()
    {
        EventManager.StartListening("OpenVault", TurnOffCursor);

        EventManager.StartListening("EndLevel", TurnOnCursor);
        EventManager.StartListening("FailedLevel", TurnOnCursor);
    }

    void OnDisable()
    {
        EventManager.StopListening("OpenVault", TurnOffCursor);

        EventManager.StopListening("EndLevel", TurnOnCursor);
        EventManager.StopListening("FailedLevel", TurnOnCursor);
    }
    
    void TurnOffCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void TurnOnCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
