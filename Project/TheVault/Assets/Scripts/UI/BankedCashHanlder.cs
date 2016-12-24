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

    [Header("Stars")]
    [SerializeField]
    private Image[] starIcons;

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

        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].enabled = false;
        }
    }
	
	void UpdateBankedCash()
    {
        float bankedTotal = LevelManager.Instance.BankedTotal;

        if (bankedTotal >= starTargets[0])
            starIcons[0].enabled = true;

        if (bankedTotal >= starTargets[1])
            starIcons[1].enabled = true;

        if (bankedTotal >= starTargets[1])
            starIcons[1].enabled = true;

        //Update Text
        bankedCashValue.text = "$" + bankedTotal.ToString("F0");

        //Update Bar
        float fillAmount = bankedTotal / starTargets[2];
        bankedCashBar.fillAmount = fillAmount;
    }
}
