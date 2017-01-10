using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineHandler : MonoBehaviour
{
    private Collider startCollider;

    private void OnEnable()
    {
        EventManager.StartListening("StartLevel", DisableStartLine);
    }

    private void OnDisable()
    {
        EventManager.StopListening("StartLevel", DisableStartLine);
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        startCollider = GetComponent<Collider>();
        startCollider.enabled = true;
        startCollider.isTrigger = true;
    }

    void StartLevel()
    {
        EventManager.TriggerEvent("StartLevel");
    }

    void DisableStartLine()
    {
        startCollider.enabled = false;
        startCollider.isTrigger = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            StartLevel();
        }                
    }
	
}
