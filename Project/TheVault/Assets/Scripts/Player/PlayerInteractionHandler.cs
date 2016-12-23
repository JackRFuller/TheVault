using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField]
    private const float playerInteractionRange = 3f;
    private Camera playerCamera;


	// Use this for initialization
	void Start ()
    {
        Init();
	}

    void Init()
    {
        playerCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update ()
    {
        SendOutRay();
	}

    void SendOutRay()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, playerInteractionRange))
        {
            if(hit.collider.tag.Equals("Bank"))
            {
                WaitForBankInput();
            }
        }
    }

    void WaitForBankInput()
    {
        //Check for Input
        if (ExtensionMehtods.GetPlayerInteractionInput())
        {
            if(LevelManager.Instance.CollectionTotal >= 0)
            {
                EventManager.TriggerEvent("TransferMoney");
            }
        }
        else
        {
            EventManager.TriggerEvent("StopTransfer");
        }
            
    }

   
}
