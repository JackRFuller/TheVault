using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : BaseMonoBehaviour
{
    [Header("Interaction Distances")]
    [SerializeField]
    private float lockDistance;
    [SerializeField]
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
           if(hit.collider.tag.Equals("Lock"))
           {
                if(ReturnDistance(hit.transform.position,transform.position) <= lockDistance)
                {
                    WaitForLockInput(hit.collider.gameObject);
                }
           }
        }
    } 
    
    private void WaitForLockInput(GameObject lockObj)
    {
        if (ExtensionMethods.GetPlayerInteractionInput())
        {
            lockObj.SendMessage("RemoveLock", SendMessageOptions.DontRequireReceiver);            
        }
    }   

    float ReturnDistance(Vector3 targetPosition, Vector3 playerPosition)
    {
        float distance = Vector3.Distance(targetPosition, playerPosition);
        return distance;
    }

   
}
