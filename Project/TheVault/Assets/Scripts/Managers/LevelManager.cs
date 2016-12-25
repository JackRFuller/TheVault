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
        EventManager.StartListening("StartLevel", StartLevel);
    }

    private void OnDisable()
    {
        EventManager.StopListening("StartLevel", StartLevel);      
    }

    private void Start()
    {
        SetupLevel();
    }

    void SetupLevel()
    {
        levelClock.timer = currentLevel.LevelStartingTime;       
    }

    /// <summary>
    /// Triggered by Start Line Handler
    /// </summary>
    void StartLevel()
    {
        levelClock.isRunning = true;
    }

    void Update()
    {
        if (levelClock.isRunning)
            RunLevelTimer();       
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
    
    /// <summary>
    /// Called when a collectible is picked
    /// up player a player
    /// </summary>
    /// <param name="value"></param>
    public void AddToCollection(int value)
    {
        collectionTotal += value;
        EventManager.TriggerEvent("CollectedMoney");          
    }
	
}
