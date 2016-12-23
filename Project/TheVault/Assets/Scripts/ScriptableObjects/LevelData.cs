using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "ScriptableObject/Level Data",order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private GameObject levelPrefab;
    public GameObject LevelPrefab
    {
        get
        {
            return levelPrefab;
        }
    }

    [SerializeField]
    private float levelStartingTime;
    public float LevelStartTime
    {
        get
        {
            return levelStartingTime;
        }
    }

    [SerializeField]
    private int[] starRatingTargets = new int[3];
    public int[] StarRatingTargets
    {
        get
        {
            return starRatingTargets;
        }
    }
}
