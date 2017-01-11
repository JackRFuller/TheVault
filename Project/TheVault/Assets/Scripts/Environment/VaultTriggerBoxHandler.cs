using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to determine whether the player is still in the vault when the timer has hit 0
/// </summary>
public class VaultTriggerBoxHandler : BaseMonoBehaviour
{
    private Collider vaultCollider;

    private void OnEnable()
    {
        EventManager.StartListening("EndLevel", TurnOnTriggerBox);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EndLevel", TurnOnTriggerBox);
    }

    // Use this for initialization
    void Start ()
    {
        vaultCollider = this.GetComponent<Collider>();

        Init();
        	
	}

    void Init()
    {
        vaultCollider.enabled = false;
        vaultCollider.isTrigger = false;
    }

    void TurnOnTriggerBox()
    {
        vaultCollider.enabled = true;
        vaultCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            EventManager.TriggerEvent("FailedLevel");
        }
    }
}
