using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ValuableHandler : Collectible
{
    [SerializeField]
    private MeshRenderer valuableMesh;
    [SerializeField]
    private AudioClip valuablePickUpSFX;

    private Collider valuableCollider;
    private AudioSource valuableAudioSource;
    

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        valuableCollider = this.GetComponent<Collider>();
        valuableAudioSource = this.GetComponent<AudioSource>();        
    }

    private void CollectValuable()
    {
        LevelManager.Instance.CollectedValuable();

        valuableCollider.enabled = false;
        valuableMesh.enabled = false;
        valuableAudioSource.Play();

        base.CollectedItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            CollectValuable();
        }
    }

}
