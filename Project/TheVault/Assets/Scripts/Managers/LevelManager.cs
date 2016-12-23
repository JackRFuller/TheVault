using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField]
    private LevelData currentLevel; //Holds Current Level Data
    public LevelData CurrentLevel { get { return currentLevel; } }

    private float collectionTotal = 0; //Tracks how much players have in their current collection
    public float CollectionTotal { get { return collectionTotal; } }
    private float bankedTotal = 0; //Tracks how much the player has banked
    public float BankedTotal { get { return bankedTotal; } }

    //Money Transfer Variables
    private bool hasIntiatedTransfer;
    private float startingCollectionValue;
    private float startingValue;
    private float targetValue;
    private float timeStarted;
    private const float transferTime = 5.0f;    


    //Level Clock
    private LevelClock levelClock;
    public LevelClock LevelTimer { get { return levelClock; } }
    public struct LevelClock
    {
        public float timer;
        public bool isRunning;
    }

    private void OnEnable()
    {
        EventManager.StartListening("TransferMoney", InitiateTransfer);     
        EventManager.StartListening("StopTransfer", StopTransfer);
    }

    private void OnDisable()
    {
        EventManager.StopListening("TransferMoney", InitiateTransfer);     
        EventManager.StopListening("StopTransfer", StopTransfer);
    }

    private void Start()
    {
        SetupLevel();
    }

    void SetupLevel()
    {
        levelClock.timer = currentLevel.LevelStartingTime;
        levelClock.isRunning = true;
    }

    void Update()
    {
        if (levelClock.isRunning)
            RunLevelTimer();

        if (hasIntiatedTransfer)
            TransferFromCollectionToBank();
    }

    void RunLevelTimer()
    {
        levelClock.timer -= Time.fixedDeltaTime;
        EventManager.TriggerEvent("RunLevelTimer");

        if(levelClock.timer < 0)
        {
            Debug.Log("Level Over");
            levelClock.isRunning = false;
            levelClock.timer = 0;
        }
    }

    
    void InitiateTransfer()
    {
        if(!hasIntiatedTransfer)
        {
            if(collectionTotal > 0)
            {
                startingCollectionValue = collectionTotal;
                startingValue = bankedTotal;               
                targetValue = bankedTotal + collectionTotal;                
                hasIntiatedTransfer = true;
                timeStarted = Time.time;
            }
        }
    }

    void TransferFromCollectionToBank()
    {
        float timeSinceStarted = Time.time - timeStarted;
        float percentageComplete = timeSinceStarted / transferTime;

        bankedTotal = Mathf.Lerp(startingValue, targetValue, percentageComplete);
        collectionTotal = Mathf.Lerp(startingCollectionValue, 0, percentageComplete);

        if(percentageComplete >= 1.0f)
        {
            bankedTotal = targetValue;
            collectionTotal = 0;

            StopTransfer();
        }
    }

    void StopTransfer()
    {
        hasIntiatedTransfer = false;
    }
    
    /// <summary>
    /// Called when a collectible is picked
    /// up player a player
    /// </summary>
    /// <param name="value"></param>
    public void AddToCollection(int value)
    {
        collectionTotal += value;
        EventManager.TriggerEvent("CollectingCash");
    }
	
}
