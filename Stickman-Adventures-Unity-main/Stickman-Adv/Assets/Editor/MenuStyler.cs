using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class MenuStyler : EditorWindow
{
    [MenuItem("Tools/Apply Premium Menu Style")]
    public static void ApplyStyle()
    {
        string scenePath = "Assets/Scenes/Menu.unity";
        var scene = EditorSceneManager.OpenScene(scenePath);

        // 1. Set Camera Background
        Camera cam = Camera.main;
        if (cam != null) {
            cam.backgroundColor = new Color(0.18f, 0.18f, 0.2f, 1f);
            cam.clearFlags = CameraClearFlags.SolidColor;
            EditorUtility.SetDirty(cam);
        }

        // 2. Add Animations
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            // Find or Add Controller
            GameObject ctrl = GameObject.Find("MenuAnimationController");
            if (ctrl == null) ctrl = new GameObject("MenuAnimationController");
            
            AttractiveUI anim = ctrl.GetComponent<AttractiveUI>();
            if (anim == null) anim = ctrl.AddComponent<AttractiveUI>();

            // Auto-Bind for common names
            GameObject title = GameObject.Find("Title"); 
            if (title == null) title = GameObject.Find("Logo");
            if (title == null) title = GameObject.Find("Name");
            
            if (title != null) anim.Title = title.GetComponent<RectTransform>();

            GameObject container = GameObject.Find("ButtonsContainer");
            if (container == null) container = GameObject.Find("Buttons");
            if (container == null) container = GameObject.Find("StartMenu");
            
            if (container != null) anim.ButtonsContainer = container;

            EditorUtility.SetDirty(anim);
        }

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        Debug.Log("Premium Menu Style Applied successfully to Menu scene!");
    }
}
