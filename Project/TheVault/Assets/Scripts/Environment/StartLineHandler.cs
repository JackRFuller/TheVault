using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineHandler : MonoBehaviour
{
    private Collider startCollider;

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
