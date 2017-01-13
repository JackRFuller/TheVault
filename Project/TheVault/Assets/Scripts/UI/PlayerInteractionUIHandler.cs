using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionUIHandler : BaseMonoBehaviour
{
    [SerializeField]
    private GameObject interactionObj;

    private void OnEnable()
    {
        EventManager.StartListening("InteractableObject", TurnOnInteractionObj);
        EventManager.StartListening("NoInteraction", TurnOffInteractionObj);
    }

    private void OnDisable()
    {
        EventManager.StopListening("InteractableObject", TurnOnInteractionObj);
        EventManager.StopListening("NoInteraction", TurnOffInteractionObj);
    }

    private void Start()
    {
        TurnOffInteractionObj();
    }

    private void TurnOnInteractionObj()
    {
        interactionObj.SetActive(true);
    }

    private void TurnOffInteractionObj()
    {
        interactionObj.SetActive(false);
    }
}
