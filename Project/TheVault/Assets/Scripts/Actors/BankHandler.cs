using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankHandler : MonoBehaviour
{
    [SerializeField]
    private AudioSource bankAudio;

    private void OnEnable()
    {
        EventManager.StartListening("TransferMoney", EnableBank);
        EventManager.StartListening("StopTransfer", DisableBank);
    }

    private void OnDisable()
    {
        EventManager.StopListening("TransferMoney", EnableBank);
        EventManager.StopListening("StopTransfer", DisableBank);
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        bankAudio.GetComponent<AudioSource>();
    }

    void EnableBank()
    {
        if (LevelManager.Instance.CollectionTotal > 0)
        {
            if (!bankAudio.isPlaying)
                bankAudio.Play();
        }
        else
        {
            DisableBank();
        }        
    }

    void DisableBank()
    {
        bankAudio.Stop();
    }
}
