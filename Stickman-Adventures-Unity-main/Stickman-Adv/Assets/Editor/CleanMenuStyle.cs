using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

public class CleanMenuStyle : EditorWindow
{
    [MenuItem("Tools/V3 - Clean Minimalist Menu")]
    public static void CleanMenu()
    {
        string scenePath = "Assets/Scenes/Menu.unity";
        var scene = EditorSceneManager.OpenScene(scenePath);

        // 1. Revert Background to a premium deep blue/purple gradient style
        Camera cam = Camera.main;
        if (cam != null) {
            cam.backgroundColor = new Color(0.1f, 0.1f, 0.15f, 1f); // Deep Midnight
            EditorUtility.SetDirty(cam);
        }

        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null) {
            Debug.LogError("No Canvas found!");
            return;
        }
        GameObject canvasObj = canvas.gameObject;

        // 2. Remove "Untidy" elements
        GameObject art = GameObject.Find("MenuArt");
        if (art != null) DestroyImmediate(art);
        
        GameObject title = GameObject.Find("MenuTitle");
        if (title != null) DestroyImmediate(title);

        // 3. Center and Clean the Buttons
        GameObject startMenu = GameObject.Find("StartMenu");
        if (startMenu != null) {
            RectTransform rt = startMenu.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.3f, 0.3f);
            rt.anchorMax = new Vector2(0.7f, 0.7f);
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;
            
            VerticalLayoutGroup vlg = startMenu.GetComponent<VerticalLayoutGroup>();
            if (vlg != null) {
                vlg.spacing = 50;
                vlg.childAlignment = TextAnchor.MiddleCenter;
            }
        }

        // 4. Style the Text (Elegance)
        foreach (var tmp in canvasObj.GetComponentsInChildren<TextMeshProUGUI>()) {
            tmp.fontStyle = FontStyles.Bold;
            tmp.characterSpacing = 15; // Elegant wide spacing
            tmp.fontSize = 45;
            tmp.color = new Color(0.9f, 0.9f, 0.95f, 1f); // Soft white
            EditorUtility.SetDirty(tmp);
        }

        // 5. Ensure Animation Controller is set up for hover effects ONLY
        GameObject ctrl = GameObject.Find("MenuAnimationController");
        if (ctrl == null) ctrl = new GameObject("MenuAnimationController");
        
        AttractiveUI anim = ctrl.GetComponent<AttractiveUI>();
        if (anim == null) anim = ctrl.AddComponent<AttractiveUI>();
        
        anim.Title = null; // No title bobbing
        anim.ButtonsContainer = startMenu;
        anim.SlideInDistance = 0; // No slide in, just clean presence
        
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        Debug.Log("V3 Minimalist Style Applied!");
    }
}
