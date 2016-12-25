using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : BaseMonoBehaviour
{
    [Header("Interaction Distances")]
    [SerializeField]
    private float bankInteractionDistance;
    [SerializeField]
    private float doorInteractionDistance;
    
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
	public override void UpdateNormal ()
    {
        SendOutRay();
	}

    void SendOutRay()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.tag.Equals("Bank"))
            {
                if(ReturnDistance(hit.transform.position, transform.position) < bankInteractionDistance)
                    WaitForBankInput();
            }

            if(hit.collider.tag.Equals("Door"))
            {
                if (ReturnDistance(hit.transform.position, transform.position) < doorInteractionDistance)
                        WaitForDoorInput(hit.transform.parent.gameObject);
            }
        }
    }

    void WaitForBankInput()
    {
        //Check for Input
        if (ExtensionMethods.GetPlayerInteractionInput())
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

    void WaitForDoorInput(GameObject doorObj)
    {
        if(ExtensionMethods.GetPlayerInteractionInput())
        {
            DoorHandler door = doorObj.GetComponent<DoorHandler>();
            door.OpenDoor();
        }
    }

    float ReturnDistance(Vector3 targetPosition, Vector3 playerPosition)
    {
        float distance = Vector3.Distance(targetPosition, playerPosition);
        return distance;
    }

   
}
