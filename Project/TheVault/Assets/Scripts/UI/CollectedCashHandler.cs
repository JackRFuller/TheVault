using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectedCashHandler : MonoBehaviour
{
    [SerializeField]
    private Text collectedCashValue;

    //Lerping Values
    private float currentCashValue;
    private float newCashValue;
    private const float countUpSpeed = 0.25f;
    private float timeStarted;
    private bool isCounting = false;

    private void OnEnable()
    {
        EventManager.StartListening("CollectingCash", UpdateCollectionValue);
        EventManager.StartListening("TransferMoney", RemoveCash);
    }

    private void OnDisable()
    {
        EventManager.StopListening("CollectingCash", UpdateCollectionValue);
        EventManager.StopListening("TransferMoney", RemoveCash);
    }

    // Use this for initialization
    void Start ()
    {
        Init();	
	}
	
	void Init()
    {
        currentCashValue = LevelManager.Instance.CollectionTotal;
        collectedCashValue.text = "$" + currentCashValue.ToString("F0");
    }

    void UpdateCollectionValue()
    {
        newCashValue = LevelManager.Instance.CollectionTotal;                
        timeStarted = Time.time;
        isCounting = true;
    }

    private void Update()
    {
        if(isCounting)
            TotalUpCash();
    }

    void TotalUpCash()
    {
        float timeSinceStarted = Time.time - timeStarted;
        float percentageComplete = timeSinceStarted / countUpSpeed;

        float cashValue = Mathf.Lerp(currentCashValue, newCashValue, percentageComplete);

        collectedCashValue.text = "$" + cashValue.ToString("F0");
        currentCashValue = (int)cashValue;

        if(percentageComplete >= 1.0f)
        {
            currentCashValue = LevelManager.Instance.CollectionTotal;
            isCounting = false;     
        }

    }

    void RemoveCash()
    {
        currentCashValue = LevelManager.Instance.CollectionTotal;
        collectedCashValue.text = "$" + currentCashValue.ToString("F0");
    }
}
