﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoSingleton<UpdateManager>
{
    private int behaviourCount;
    private BaseMonoBehaviour[] behaviours;

    public static void AddBehaviour(BaseMonoBehaviour behaviour)
    {
        if (Instance.behaviours == null)
        {
            Instance.behaviours = new BaseMonoBehaviour[1];
        }
        else
        {
            System.Array.Resize(ref Instance.behaviours, Instance.behaviours.Length + 1);
        }

        Instance.behaviours[Instance.behaviours.Length - 1] = behaviour;
        Instance.behaviourCount = Instance.behaviours.Length;
    }

    public static void RemoveBehaviour(BaseMonoBehaviour behaviour)
    {
        int addAtIndex = 0;
        BaseMonoBehaviour[] tempBehavioursArray = new BaseMonoBehaviour[Instance.behaviours.Length - 1];

        for (int i = 0; i < Instance.behaviours.Length; i++)
        {
            if (Instance.behaviours[i] == null)
            {
                continue;
            }
            else if (Instance.behaviours[i] == behaviour)
            {
                continue;
            }

            tempBehavioursArray[addAtIndex] = Instance.behaviours[i];
            addAtIndex++;
        }

        Instance.behaviours = new BaseMonoBehaviour[tempBehavioursArray.Length];

        for (int i = 0; i < tempBehavioursArray.Length; i++)
        {
            Instance.behaviours[i] = tempBehavioursArray[i];
        }

        Instance.behaviourCount = Instance.behaviours.Length;
    }

    private void Update()
    {
        if(behaviourCount > 0)
        {
            for(int i = 0; i < behaviours.Length; i++)
            {
                if(behaviours[i] == null || !behaviours[i].isActiveAndEnabled)
                {
                    continue;
                }

                behaviours[i].UpdateNormal();
            }
        }
    }

    private void FixedUpdate()
    {
        if (behaviourCount > 0)
        {
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i] == null || !behaviours[i].isActiveAndEnabled)
                {
                    continue;
                }

                behaviours[i].UpdateFixed();
            }
        }
    }

    private void LateUpdate()
    {
        if (behaviourCount > 0)
        {
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i] == null || !behaviours[i].isActiveAndEnabled)
                {
                    continue;
                }

                behaviours[i].UpdateLate();
            }
        }
    }
}
