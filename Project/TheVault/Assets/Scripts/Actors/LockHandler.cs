using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LockHandler : BaseMonoBehaviour
{
    private AudioSource lockAudioSource;

    [SerializeField]
    private Image lockBar;

    private Collider lockCollider;

    private const float unlockRate = 0.3f;
    private float unlockStatus = 1;

    [Header("Wire Settings")]
    [SerializeField]
    private MeshRenderer wire;
    [SerializeField]
    private Material enabledWire;
    [SerializeField]
    private Material disabledWire;
   

    private void Start()
    {
        lockCollider = this.GetComponent<Collider>();
        lockAudioSource = this.GetComponent<AudioSource>();

        if(wire)
        {
            wire.material = enabledWire;
        }
        else
        {
            Debug.LogError("No Wire for " + gameObject.name);
        }
       
    }

    public void RemoveLock()
    {
        if (!lockAudioSource.isPlaying)
            lockAudioSource.Play();

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
        wire.material = disabledWire;
        lockBar.fillAmount = 0;
        lockCollider.enabled = false;
        EventManager.TriggerEvent("Unlock");
    }
	
}
