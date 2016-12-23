using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankedCashHanlder : MonoBehaviour
{
    [SerializeField]
    private Text bankedCashValue;
    [SerializeField]
    private Image bankedCashBar;

    private float[] starTargets;

    private void OnEnable()
    {
        EventManager.StartListening("TransferMoney", UpdateBankedCash);
    }

    private void OnDisable()
    {
        EventManager.StopListening("TransferMoney", UpdateBankedCash);
    }

    // Use this for initialization
    void Start ()
    {
        Init();
	}

    void Init()
    {
        bankedCashValue.text = "$" + LevelManager.Instance.BankedTotal.ToString("F0");
        starTargets = LevelManager.Instance.CurrentLevel.StarRatingTargets;
    }
	
	void UpdateBankedCash()
    {
        float bankedTotal = LevelManager.Instance.BankedTotal;

        //Update Text
        bankedCashValue.text = "$" + bankedTotal.ToString("F0");

        //Update Bar
        float fillAmount = bankedTotal / starTargets[2];
        bankedCashBar.fillAmount = fillAmount;
    }
}
