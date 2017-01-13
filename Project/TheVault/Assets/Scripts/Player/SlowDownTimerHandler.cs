using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownTimerHandler : BaseMonoBehaviour
{
    private bool canSlowDownTime;

    [SerializeField]
    private float slowDownAmount;
    [SerializeField]
    private float slowDownRechargeRate;
    [SerializeField]
    private float slowDownCooldownRate;

    private static float slowDownAvailable;
    public static float SlowDownAvailable { get { return slowDownAvailable; } }
    private bool isCoolingDown;

    private bool isSlowingTime;
    private bool hasStartedCooldownProcess;
    

    private void OnEnable()
    {
        EventManager.StartListening("EndLevel", DisableSlowDownTime);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EndLevel", DisableSlowDownTime);
    }

    private void Start()
    {
        EnableSlowDownTime();   
    }

    private void EnableSlowDownTime()
    {
        canSlowDownTime = true;
        slowDownAvailable = slowDownAmount;

        EventManager.TriggerEvent("SetupSlowMo");
    }

    private void DisableSlowDownTime()
    {
        canSlowDownTime = false;
        Time.timeScale = 1.0f;
    }

    public override void UpdateNormal()
    {
        if (!canSlowDownTime)
            return;

        SlowDownTimeInput();
    }

    private void SlowDownTimeInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isSlowingTime = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isSlowingTime = false;          
        }

        if (isSlowingTime)
        {
            SlowDownTime();
        }
        else
        {
            if(!isCoolingDown)
                if(slowDownAvailable < slowDownAmount)
                    ReturnToNormalTime();
        }

        if(isCoolingDown)
        {
            RechargeSlowMo();
        }
    }

    private void SlowDownTime()
    {      
        isCoolingDown = false;

        if (Time.timeScale == 1.0f)
            Time.timeScale = 0.3f;

        slowDownAvailable -= Time.fixedDeltaTime;
        StopCoroutine(StartCooldownProcess());
        hasStartedCooldownProcess = false;

        if (slowDownAvailable <= 0)
        {
            canSlowDownTime = false;
            ReturnToNormalTime();            
        }

        EventManager.TriggerEvent("UsingSlowMotion");
        
    }

    private void RechargeSlowMo()
    {
        slowDownAvailable += slowDownRechargeRate * Time.fixedDeltaTime;
        EventManager.TriggerEvent("UsingSlowMotion");

        if (slowDownAvailable >= slowDownAmount)
        {
            slowDownAvailable = slowDownAmount;
            isCoolingDown = false;
        }

        
    }

    private void ReturnToNormalTime()
    {
        if(!hasStartedCooldownProcess)
        {
            Time.timeScale = 1.0f;
            StartCoroutine(StartCooldownProcess());
        }
    }

    private IEnumerator StartCooldownProcess()
    {
        hasStartedCooldownProcess = true;
        yield return new WaitForSeconds(slowDownCooldownRate);
        isCoolingDown = true;        
    }



}
