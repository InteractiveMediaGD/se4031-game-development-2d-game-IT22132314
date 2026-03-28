using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private EnemyController boss;
    private GameOverMenu gameOverMenu;
    private bool IsGameOver;
    private bool IsLoadingLevel; 

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boss = GameObject.FindWithTag("Enemy").GetComponent<EnemyController>();
        gameOverMenu = GameObject.Find("Canvas").GetComponent<GameOverMenu>();
    }

    void Update()
    {
        if (IsGameOver || IsLoadingLevel)
        {
            return;
        }

        if (player.IsPlayerDead())
        {
            IsGameOver = true;
            LevelStateHolder.ResetLevel();
            gameOverMenu.StartGameOverMenu();
        }

        if (boss != null && boss.IsDead())
        {
            IsLoadingLevel = true; // Set flag so we don't call this again
            Debug.Log("Boss Defeated! Loading Next Level...");
            
            if (LevelLoader.Instance != null) {
                LevelLoader.Instance.LoadNextScene();
            } else {
                Debug.LogError("Critical Error: LevelLoader.Instance is NULL! Cannot transition to next level.");
                // Fallback: Just load the victory scene immediately if the loader is missing
                UnityEngine.SceneManagement.SceneManager.LoadScene("LevelProgressScene");
            }
        }
    }
}
