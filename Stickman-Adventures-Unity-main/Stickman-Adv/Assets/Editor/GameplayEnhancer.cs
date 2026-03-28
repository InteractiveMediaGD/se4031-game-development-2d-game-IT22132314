using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameplayEnhancer
{
    [MenuItem("Tools/Upgrade Player Jump and Health")]
    public static void UpgradeGameplay()
    {
        string[] levels = { 
            "Assets/Scenes/Level-1.unity", 
            "Assets/Scenes/Level-2.unity", 
            "Assets/Scenes/Level-3.unity" 
        };

        foreach (string levelPath in levels)
        {
            var scene = EditorSceneManager.OpenScene(levelPath);
            
            // 1. Boost Player Jump Height
            PlayerController player = Object.FindObjectOfType<PlayerController>();
            if (player != null)
            {
                player.JumpSpeed = 18f; // Final, massive jump!
                EditorUtility.SetDirty(player);
                Debug.Log("Upgraded Jump Speed for player in: " + levelPath);
            }

            // 2. Clear old HealthPacks and Obstacles if any (Search by name to avoid Tag errors)
            GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
            foreach(var p in allObjects) {
                if (p.name == "HealthPack" || p.name == "SpikeObstacle") {
                    Object.DestroyImmediate(p);
                }
            }

            // 3. Find Enemies and place Health Packs behind them, and Spikes before the packs
            EnemyController[] enemies = Object.FindObjectsOfType<EnemyController>();
            Sprite healthSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Graphics/Others/Health_pack.png");
            Sprite obstacleSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Graphics/Others/SharpSpikes.png");
            
            foreach (var enemy in enemies)
            {
                // -- ENEMY POSITIONING BASIS --
                Vector3 enemyPos = enemy.transform.position;

                // -- CREATE SPIKE OBSTACLE --
                GameObject obstacle = new GameObject("SpikeObstacle");
                obstacle.transform.position = enemyPos + new Vector3(2.5f, -0.5f, 0f); // Position BEFORE the health pack, close to ground
                
                var osr = obstacle.AddComponent<SpriteRenderer>();
                osr.sprite = obstacleSprite;
                osr.sortingOrder = 5; // Ensure it's visible
                obstacle.transform.localScale = new Vector3(0.5f, 0.5f, 1f); // Scale as needed
                
                var ocol = obstacle.AddComponent<BoxCollider2D>();
                ocol.isTrigger = true;
                obstacle.AddComponent<Obstacle>(); // Applies damage logic
                obstacle.tag = "Finish"; // Generic tag since Spike tag might not exist

                // -- CREATE HEALTH PACK --
                GameObject pack = new GameObject("HealthPack");
                pack.transform.position = enemyPos + new Vector3(5.0f, 0f, 0f); // Position BEHIND the enemy and spike
                
                var psr = pack.AddComponent<SpriteRenderer>();
                psr.sprite = healthSprite;
                psr.sortingOrder = 5;
                pack.transform.localScale = new Vector3(0.25f, 0.25f, 1f); // Scale for better visibility
                
                var pcol = pack.AddComponent<BoxCollider2D>();
                pcol.isTrigger = true;
                pack.AddComponent<HealthPack>();
                pack.tag = "Finish"; 
                
                Debug.Log($"Placed Health Pack and Spike behind: {enemy.name}");
            }
            
            EditorSceneManager.SaveScene(scene);
        }
        
        Debug.Log("Gameplay Upgrade Complete! New Health Packs and Spikes placed in all levels.");
    }
}
