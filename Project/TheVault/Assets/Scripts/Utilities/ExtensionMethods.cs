﻿using UnityEngine;
using System.Collections;

public class ExtensionMehtods : MonoBehaviour
{
    #region Inputs

    public static bool GetPlayerInteractionInput()
    {
        if (Input.GetButton("Interact"))
        {
            return true;
        }            
        else
        {
            return false;
        }
       

        
    }

#endregion

    public static string FormatTimeText(float time)
    {
        string formattedTime = string.Empty;

        var intTime = (int)Mathf.Floor(time);
        var minutes = intTime / 60;
        var seconds = intTime % 60;
        var fraction = time * 100;
        fraction = fraction % 100;
        formattedTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);

        return formattedTime;
    }
}