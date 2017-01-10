using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionHandler : BaseMonoBehaviour
{
    [SerializeField]
    private GameObject infoObj;
    [SerializeField]
    private GameObject controlObj;    

    public void OnClickTurnOnInfoOptions()
    {
        infoObj.SetActive(true);
        controlObj.SetActive(false);
    }

    public void OnClickTurnOnControlOptions()
    {
        infoObj.SetActive(false);
        controlObj.SetActive(true);
    }

}
