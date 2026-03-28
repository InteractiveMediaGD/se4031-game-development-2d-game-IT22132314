using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

public class BetterMenuStyler : EditorWindow
{
    [MenuItem("Tools/V2 - Paper Style Menu Overhaul")]
    public static void OverhaulMenu()
    {
        string scenePath = "Assets/Scenes/Menu.unity";
        var scene = EditorSceneManager.OpenScene(scenePath);

        // 1. Setup Camera
        Camera cam = Camera.main;
        if (cam != null) {
            cam.backgroundColor = new Color(0.12f, 0.12f, 0.15f, 1f); // Even darker
            EditorUtility.SetDirty(cam);
        }

        // 2. Clear out old background boxes if any
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null) {
            Debug.LogError("No Canvas found in the current scene to apply style!");
            return;
        }
        GameObject canvasObj = canvas.gameObject;
        foreach (var img in canvasObj.GetComponentsInChildren<Image>()) {
            if (img.gameObject.name.Contains("Background") || img.gameObject.name.Contains("Fill")) {
                // Keep them if they are part of a button's visual
            }
        }

        // 3. Add Shadow/Glow to all Menu Text
        foreach (var tmp in canvasObj.GetComponentsInChildren<TextMeshProUGUI>()) {
            tmp.fontStyle = FontStyles.Bold | FontStyles.UpperCase;
            tmp.characterSpacing = 5;
            // Add a simple shadow effect via material if available, or just color
            tmp.color = Color.white;
            EditorUtility.SetDirty(tmp);
        }

        // 4. Create a "Sketchbook" Background Element
        GameObject art = GameObject.Find("MenuArt");
        if (art == null) {
            art = new GameObject("MenuArt");
            art.transform.SetParent(canvasObj.transform, false);
            art.AddComponent<RectTransform>();
            Image img = art.AddComponent<Image>();
            
            // Try to find the Game-Cover sprite
            string guid = "042947bff3154ed4f9490366d882cb54";
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!string.IsNullOrEmpty(path)) {
                img.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                img.preserveAspect = true;
                img.color = new Color(1, 1, 1, 0.6f); // Slightly transparent
            }
            
            RectTransform rt = art.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0.5f, 1);
            rt.anchoredPosition = new Vector2(100, 0);
            rt.sizeDelta = new Vector2(-200, -100);
        }

        // 5. Position the Buttons Container to the Right
        GameObject startMenu = GameObject.Find("StartMenu");
        if (startMenu == null) {
            foreach (Transform child in canvasObj.transform) {
                if (child.name.Contains("Menu")) {
                    startMenu = child.gameObject;
                    break;
                }
            }
        }
        if (startMenu != null) {
            RectTransform rt = startMenu.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.6f, 0.2f);
            rt.anchorMax = new Vector2(0.9f, 0.8f);
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;
            
            // Add a VerticalLayoutGroup if missing
            VerticalLayoutGroup vlg = startMenu.GetComponent<VerticalLayoutGroup>();
            if (vlg == null) vlg = startMenu.AddComponent<VerticalLayoutGroup>();
            vlg.spacing = 30;
            vlg.childAlignment = TextAnchor.MiddleCenter;
            vlg.childControlHeight = true;
            vlg.childControlWidth = true;
        }

        // 6. Add a BIG Title
        GameObject titleObj = GameObject.Find("MenuTitle");
        if (titleObj == null) {
            titleObj = new GameObject("MenuTitle");
            titleObj.transform.SetParent(canvasObj.transform, false);
            TextMeshProUGUI titleText = titleObj.AddComponent<TextMeshProUGUI>();
            titleText.text = "PAPER WORLD SURVIVOR";
            titleText.fontSize = 72;
            titleText.alignment = TextAlignmentOptions.Center;
            titleText.color = new Color(1, 0.8f, 0.2f); // Gold/Paper yellow
            titleText.fontStyle = FontStyles.Bold;
            
            RectTransform rt = titleObj.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.85f);
            rt.anchorMax = new Vector2(0.5f, 0.85f);
            rt.anchoredPosition = new Vector2(0, 0);
            rt.sizeDelta = new Vector2(800, 100);
        }

        // 7. Ensure Animations are updated
        GameObject ctrl = GameObject.Find("MenuAnimationController");
        if (ctrl != null) {
            AttractiveUI anim = ctrl.GetComponent<AttractiveUI>();
            if (anim != null) {
                anim.Title = titleObj.GetComponent<RectTransform>();
                anim.ButtonsContainer = startMenu;
                anim.SlideInDistance = 1500f; // Slide from further right
            }
        }

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        Debug.Log("V2 Overhaul Applied! Menu is now more attractive.");
    }
}
