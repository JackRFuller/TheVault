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
    public float LevelStartingTime
    {
        get
        {
            return levelStartingTime;
        }
    }

    [SerializeField]
    private float[] starRatingTargets = new float[3];
    public float[] StarRatingTargets
    {
        get
        {
            return starRatingTargets;
        }
    }

    [Header("Valuable")]
    [SerializeField]
    private bool isValuable;
    public bool IsValuable { get { return isValuable; } }
    [SerializeField]
    private int numberOfKeys;
    public int NumberOfKeys { get { return numberOfKeys; } }
    [SerializeField]
    private GameObject valuablePrefab;
    public GameObject ValuablePrefab { get { return valuablePrefab; } }

    [Header("Materials")]
    [SerializeField]
    private Material disabledMaterial;
    public Material DisabledMaterial { get { return disabledMaterial; } }
    [SerializeField]
    private Material enabledMaterial;
    public Material EnabledMaterial { get { return enabledMaterial; } }

    [Header("Text Attributes")]
    [SerializeField]
    private string levelTitle;
    public string LevelTitle { get { return levelTitle; } }
}
