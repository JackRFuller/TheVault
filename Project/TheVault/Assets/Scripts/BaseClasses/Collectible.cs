using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : BaseMonoBehaviour
{
    [SerializeField]
    protected int itemValue = 1000;
    public int ItemValue { get { return itemValue; } }

    protected virtual void CollectedItem()
    {
        LevelManager.Instance.AddToCollection(itemValue);
    }
}

	

