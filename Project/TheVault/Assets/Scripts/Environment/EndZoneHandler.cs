using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZoneHandler : BaseMonoBehaviour
{
    private Collider endCollider;

    private void OnEnable()
    {
        EventManager.StartListening("StartLevel", EnableEndZone);
    }

    private void OnDisable()
    {
        EventManager.StopListening("StartLevel", EnableEndZone);
    }

    private void Start()
    {
        endCollider = this.GetComponent<Collider>();
        endCollider.enabled = false;
        endCollider.isTrigger = false;
    }

    private void EnableEndZone()
    {
        endCollider.enabled = true;
        endCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        EventManager.TriggerEvent("EndLevel");
    }
}
