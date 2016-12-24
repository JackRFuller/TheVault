using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private Animator doorAnim;
    private AudioSource doorAudio;


	// Use this for initialization
	void Start ()
    {
        Init();
	}

    void Init()
    {
        doorAnim = GetComponent<Animator>();
        doorAudio = GetComponent<AudioSource>();
    }
	
	public void OpenDoor()
    {
        doorAnim.enabled = true;
        if(!doorAudio.isPlaying)
            doorAudio.Play();
    }
}
