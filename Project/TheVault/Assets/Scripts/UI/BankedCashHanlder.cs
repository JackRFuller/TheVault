using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankedCashHanlder : BaseMonoBehaviour
{
    [SerializeField]
    private Text bankedCashValue;
    [SerializeField]
    private Image bankedCashBar;

    [Header("Star Transforms")]
    [SerializeField]
    private RectTransform[] starTransforms;

    [Header("Stars")]
    [SerializeField]
    private Image[] starIcons;

    [Header("Target Scores")]
    [SerializeField]
    private Text[] targetScores;

    private float[] starTargets;

    //Banking Money
    private float newTotal;
    private float oldTotal;
    private bool isTotaling;
    private float timeStarted;
    private const float totalSpeed = 0.2f;
    private float oldFillAmount;
    private float newFillAmount;

    private void OnEnable()
    {
        EventManager.StartListening("CollectedMoney", InitialiseBankUpdate);
    }

    private void OnDisable()
    {
        EventManager.StopListening("CollectedMoney", InitialiseBankUpdate);
    }

    // Use this for initialization
    void Start ()
    {
        Init();
        PositionStarScores();
	}

    void Init()
    {
        bankedCashValue.text = "$" + LevelManager.Instance.BankedTotal.ToString("F0");
        starTargets = LevelManager.Instance.CurrentLevel.StarRatingTargets;

        for (int i = 0; i < targetScores.Length; i++)
        {
            targetScores[i].text = starTargets[i].ToString("F0");
        }

        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].enabled = false;
        }

        oldTotal = 0;
        oldFillAmount = bankedCashBar.fillAmount;
    }

    private void PositionStarScores()
    {
        //Place Stars
        float[] percentages = new float[3];
        for (int i = 0; i < starTargets.Length; i++)
        {
            percentages[i] = starTargets[i] / starTargets[2];
            Debug.Log(percentages[i]);
        }

        RectTransform bar = bankedCashBar.GetComponent<RectTransform>();

        Vector3 startPosition = bar.localPosition;

        Vector3 endPosition = new Vector3(startPosition.x + bar.sizeDelta.x,
                                          startPosition.y,
                                          startPosition.z);

        float difference = endPosition.x - startPosition.x;

        for (int i = 0; i < starTransforms.Length; i++)
        {
            //Work out percentage of score in reference to the difference
            float offset = difference * percentages[i];
            Vector3 newPosition = new Vector3(startPosition.x + offset,
                                          starTransforms[i].localPosition.y,
                                          startPosition.z);

            starTransforms[i].localPosition = newPosition;
        }
    }

    void Update()
    {
        if(isTotaling)
            TotalMoney();
    }

    void InitialiseBankUpdate()
    {
        oldFillAmount = bankedCashBar.fillAmount;
        newTotal = LevelManager.Instance.CollectionTotal;
        newFillAmount = newTotal / starTargets[2];
        isTotaling = true;
        timeStarted = Time.time;
        
    }

    void TotalMoney()
    {
        float timeSinceStarted = Time.time - timeStarted;
        float percentageComplete = timeSinceStarted / totalSpeed;

        float total = Mathf.Lerp(oldTotal, newTotal, percentageComplete);
        float fillAmount = Mathf.Lerp(oldFillAmount, newFillAmount, percentageComplete);

        //Update Bar
        bankedCashBar.fillAmount = fillAmount;

        //Update Text
        bankedCashValue.text = "$" + total.ToString("F0");

        CheckStars(total);

        if(percentageComplete >= 1.0f)
        {
            StopTotalling();
        }
    }

    void StopTotalling()
    {
        isTotaling = false;
        oldTotal = LevelManager.Instance.CollectionTotal;
        oldFillAmount = bankedCashBar.fillAmount;
    }

    void CheckStars(float total)
    {
        if (total >= starTargets[0])
            starIcons[0].enabled = true;

        if (total >= starTargets[1])
            starIcons[1].enabled = true;

        if (total >= starTargets[2])
            starIcons[2].enabled = true;
    }	
}
