using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldHandler : BaseMonoBehaviour
{
    [SerializeField]
    private Collider forcefieldCollider;
    [SerializeField]
    private MeshRenderer forcefieldMesh;

    [SerializeField]
    private Transform valuableSpawnPoint;    
    private int numberOfLocks; //Determines how many locks the player has to activate in order to unlock forcefield

    private void OnEnable()
    {
        EventManager.StartListening("Unlock", RemoveLock);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Unlock", RemoveLock);
    }

    private void Start()
    {
        GetNumberOfLocks();
        SpawnInValuable();
    }

    private void GetNumberOfLocks()
    {
        numberOfLocks = LevelManager.Instance.CurrentLevel.NumberOfKeys;
    }

    private void SpawnInValuable()
    {
        GameObject valuable = LevelManager.Instance.CurrentLevel.ValuablePrefab;
        valuable = Instantiate(valuable, valuableSpawnPoint.position, Quaternion.identity) as GameObject;
    }

    private void RemoveLock()
    {
        numberOfLocks--;

        if(numberOfLocks <= 0)
        {
            UnlockForceField();
        }
    }

    private void UnlockForceField()
    {
        forcefieldCollider.enabled = false;
        forcefieldMesh.enabled = false;
    }
    
}
