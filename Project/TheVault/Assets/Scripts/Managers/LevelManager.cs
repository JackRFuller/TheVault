using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField]
    private LevelData currentLevel; //Holds Current Level Data
    public LevelData CurrentLevel { get { return currentLevel; } }

    [Header("Player")]
    [SerializeField]
    private GameObject playerPrefab;
    private GameObject player;
    [SerializeField]
    private Transform playerSpawnPoint;

    //Level
    private GameObject level;

    //Timer Variables
    private static float levelTimer;
    public static float LevelTimer { get { return levelTimer; } }
    private bool isLevelTimerRunning = false;  

    //Money Variables
    private static int moneyCollected;
    public static int MoneyCollected { get { return moneyCollected; } }

    //Valuable variables
    private bool hasCollectedValuable; //tracks if player has collected valuable    
    public bool HasCollectedValuable { get { return hasCollectedValuable; } }


    private void OnEnable()
    {
        EventManager.StartListening("SetupLevel", SetupLevel);
        EventManager.StartListening("StartLevel", StartLevel);
        EventManager.StartListening("ExittedLevel", EndLevel);   
    }

    private void OnDisable()
    {
        EventManager.StopListening("SetupLevel", SetupLevel);
        EventManager.StopListening("StartLevel", StartLevel);
        EventManager.StopListening("ExittedLevel", EndLevel);
    }

    void SetupLevel()
    {
        //Load In Player
        if (!player)
            player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity) as GameObject;
        else
        {
            player.transform.position = playerSpawnPoint.position;
            player.transform.rotation = Quaternion.identity;
        }

        if (level)
            Destroy(level);

        //Load in Level
        level = Instantiate(currentLevel.LevelPrefab, currentLevel.LevelPrefab.transform.position, Quaternion.identity) as GameObject;

        levelTimer = currentLevel.LevelStartingTime;

        EventManager.TriggerEvent("OpenVault");  
    }

    /// <summary>
    /// Triggered by Start Line Handler
    /// </summary>
    void StartLevel()
    {
        isLevelTimerRunning = true;
    }

    /// <summary>
    /// Triggered by End Zone Handler
    /// </summary>
    void EndLevel()
    {
        isLevelTimerRunning = false;
    }

    public override void UpdateNormal()
    {
        if(isLevelTimerRunning)
            RunLevelTimer();       
    }

    void RunLevelTimer()
    {
        levelTimer -= Time.fixedDeltaTime;
        EventManager.TriggerEvent("RunLevelTimer");

        if (levelTimer <= 0)
        {
            EndLevel();
            EventManager.TriggerEvent("EndLevel");
        }
    }
    
    /// <summary>
    /// Called when a collectible is picked
    /// up player a player
    /// </summary>
    /// <param name="value"></param>
    public void AddToCollection(int value)
    {
        moneyCollected += value;
        EventManager.TriggerEvent("CollectedMoney");          
    }

    public void CollectedValuable()
    {
        hasCollectedValuable = true;
    }
}
