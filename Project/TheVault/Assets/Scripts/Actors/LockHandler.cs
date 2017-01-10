using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LockHandler : BaseMonoBehaviour
{
    [SerializeField]
    private Image lockBar;

    private Collider lockCollider;

    private const float unlockRate = 0.3f;
    private float unlockStatus = 1;

    private void Start()
    {
        lockCollider = GetComponent<Collider>();
    }

    private void RemoveLock()
    {
        unlockStatus -= unlockRate * Time.deltaTime;

        //Set Canvas Status
        lockBar.fillAmount = unlockStatus;

        if(unlockStatus <= 0)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        lockBar.fillAmount = 0;
        lockCollider.enabled = false;
        EventManager.TriggerEvent("Unlock");
    }
	
}
