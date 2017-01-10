using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : BaseMonoBehaviour
{
    private Image reticleImage;

    private void OnEnable()
    {
        EventManager.StartListening("OpenVault", TurnOnReticle);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OpenVault", TurnOnReticle);
    }

    private void Start()
    {
        reticleImage = this.GetComponent<Image>();
        reticleImage.enabled = false;
    }

    void TurnOffReticle()
    {
        reticleImage.enabled = false;
    }

    void TurnOnReticle()
    {
        reticleImage.enabled = true;
    }

    void Temp()
    {

    }
}
