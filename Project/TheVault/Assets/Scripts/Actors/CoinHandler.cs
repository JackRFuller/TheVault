using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CoinHandler : Collectible
{
    
    [SerializeField]
    private MeshRenderer coinMesh;
    private Collider coinCollider;
    private AudioSource coinAudio;

    [SerializeField]
    private AudioClip coinPickUpClip;

    // Use this for initialization
    void Start ()
    {
        Init();
	}

    void Init()
    {
        coinAudio = GetComponent<AudioSource>();
        coinCollider = GetComponent<Collider>();
        coinCollider.enabled = true;
        coinCollider.isTrigger = true;
        coinMesh.enabled = true;
    }

    /// <summary>
    /// Triggered when the player hits the
    /// coins trigger
    /// </summary>
    void CollectCoin()
    {
        coinCollider.enabled = false;
        coinCollider.isTrigger = false;
        coinMesh.enabled = false;

        coinAudio.clip = coinPickUpClip;
        coinAudio.Play();

        //Adds Value to LM
        base.CollectedItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            CollectCoin();
        }
    }
}
