using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using TMPro; // TextMeshPro

public class AutoSetupScoreUI
{
    [MenuItem("Tools/Auto Setup Score System")]
    public static void SetupScoreUI()
    {
        // Safety Check: You cannot change scenes while the game is hitting Play!
        if (Application.isPlaying)
        {
            EditorUtility.DisplayDialog("Error", "Please STOP the game (hit the Play button to turn it off) before running this tool!", "Ok");
            return;
        }

        // Define the target levels
        string[] levels = { 
            "Assets/Scenes/Level-1.unity", 
            "Assets/Scenes/Level-2.unity", 
            "Assets/Scenes/Level-3.unity" 
        };

        foreach (string levelPath in levels)
        {
            var scene = EditorSceneManager.OpenScene(levelPath);

            // Destroy the old badly formatted ones instantly so we can completely rebuild it cleanly!
            GameObject oldManager = GameObject.Find("ScoreManager");
            if (oldManager != null) GameObject.DestroyImmediate(oldManager);
            GameObject oldText = GameObject.Find("Score Text");
            if (oldText != null) GameObject.DestroyImmediate(oldText);

            // 1. Create the new Manager
            GameObject scoreManagerObj = new GameObject("ScoreManager");
            var scoreManagerScript = scoreManagerObj.AddComponent<ScoreManager>();
            
            // 2. Find the MAIN root Canvas (avoiding the mini-canvases inside dialogue bubbles)
            Canvas targetCanvas = null;
            Canvas[] allCanvases = Object.FindObjectsOfType<Canvas>();
            foreach (Canvas c in allCanvases) {
                // We want the main overlay canvas, not a nested one or a world-space one
                if (c.isRootCanvas && c.renderMode == RenderMode.ScreenSpaceOverlay) {
                    targetCanvas = c;
                    break;
                }
            }
            // Fallback to name-based if we can't find by properties
            if (targetCanvas == null) targetCanvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();
            if (targetCanvas == null && allCanvases.Length > 0) targetCanvas = allCanvases[0];
            
            if (targetCanvas != null)
            {
                // 3. Create Text with High Visibility
                GameObject textObj = new GameObject("Score Text");
                textObj.transform.SetParent(targetCanvas.transform, false);
                // Ensure it stays on top of EVERY other UI element
                textObj.transform.SetAsLastSibling(); 

                var txt = textObj.AddComponent<TextMeshProUGUI>();
                
                txt.text = "Score: 0";
                txt.fontSize = 32; 
                txt.fontStyle = FontStyles.Bold;
                txt.alignment = TextAlignmentOptions.TopLeft;
                txt.color = new Color(1f, 0.92f, 0.016f); // Bright Gold/Yellow
                
                // Add a sharp Black Outline for visibility against any background
                txt.outlineWidth = 0.25f;
                txt.outlineColor = Color.black;
                
                txt.enableWordWrapping = false;
                
                // 4. Position and Anchor (MOVING TO TOP-CENTER TO AVOID OVERLAP)
                RectTransform rect = textObj.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(400, 100); 
                
                // Anchoring to Top-Center
                rect.anchorMin = new Vector2(0.5f, 1);
                rect.anchorMax = new Vector2(0.5f, 1);
                rect.pivot = new Vector2(0.5f, 1);
                
                // Position it clearly at the top center
                rect.anchoredPosition = new Vector2(0, -30); 
                
                txt.alignment = TextAlignmentOptions.Center;
                txt.color = new Color(1f, 1f, 0f, 1f); // Bright Yellow
                
                // Thick black outline for maximum pop!
                txt.outlineWidth = 0.3f;
                txt.outlineColor = Color.black;

                scoreManagerScript.scoreText = txt;
            }
            
            EditorSceneManager.SaveScene(scene);
        }
        
        Debug.Log("Score UI Setup complete! All levels have been automatically updated.");
    }
}
