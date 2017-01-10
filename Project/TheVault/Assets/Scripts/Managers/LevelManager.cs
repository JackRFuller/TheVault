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

    private float bankedTotal;
    public float BankedTotal { get { return bankedTotal; } }

    private bool hasCollectedValuable; //tracks if player has collected valuable    
    public bool HasCollectedValuable { get { return hasCollectedValuable; } }
    
    //Level Clock
    private LevelClock levelClock;
    public LevelClock LevelTimer { get { return levelClock; } }
    public struct LevelClock
    {
        public float timer;
        public bool isRunning;
    }

    [Header("Player")]
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject player;
    [SerializeField]
    private Transform playerSpawnPoint;

    //Level
    private GameObject level;

    private void OnEnable()
    {
        EventManager.StartListening("SetupLevel", SetupLevel);
        EventManager.StartListening("StartLevel", StartLevel);
        EventManager.StartListening("EndLevel", EndLevel);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SetupLevel", SetupLevel);
        EventManager.StopListening("StartLevel", StartLevel);
        EventManager.StopListening("EndLevel", EndLevel);
    }

    void SetupLevel()
    {
        //Load In Player
        if (!player)
            player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity) as GameObject;

        //Load in Level
        level = Instantiate(currentLevel.LevelPrefab, currentLevel.LevelPrefab.transform.position, Quaternion.identity) as GameObject;

        levelClock.timer = currentLevel.LevelStartingTime;

        EventManager.TriggerEvent("OpenVault");  
    }

    /// <summary>
    /// Triggered by Start Line Handler
    /// </summary>
    void StartLevel()
    {
        levelClock.isRunning = true;
    }

    /// <summary>
    /// Triggered by End Zone Handler
    /// </summary>
    void EndLevel()
    {
        levelClock.isRunning = false;
        Debug.Log("End Level");
    }

    public override void UpdateNormal()
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

    public void CollectedValuable()
    {
        hasCollectedValuable = true;
    }
}
