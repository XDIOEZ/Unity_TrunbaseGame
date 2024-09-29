using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitiationSystem : MonoBehaviour
{
    // Singleton Instance
    public static BattleInitiationSystem Instance { get; private set; }

    // The enemy to face in battle
    public Enemy enemy;

    // Delegate and event for starting the battle
    public delegate void BattleStartHandler();
    public event BattleStartHandler OnBattleStart;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void StartBattle()
    {
        // Optionally set up the enemy's Level_Battlecharacter data here
        // enemy.characterData = waitBattleCharacterData;

        // Trigger the battle start event
        OnBattleStart?.Invoke();

        // Move the camera to a new Position
        MoveCameraToBattlePosition();
    }

    private void MoveCameraToBattlePosition()
    {
        // Move the camera to the specified Position
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }
}
