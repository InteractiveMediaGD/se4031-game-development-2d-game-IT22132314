using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;

public class BetterSketchyStyler : EditorWindow
{
    [MenuItem("Tools/V4 - Sketchy Styled Menu")]
    public static void SketchMenu()
    {
        // 1. Prepare Sprites
        string btnPath = "Assets/Graphics/UI/SketchyButton.png";
        string bgPath = "Assets/Graphics/UI/SketchyBackground.png";
        
        ConfigureAsSprite(btnPath);
        ConfigureAsSprite(bgPath);
        
        Sprite btnSprite = AssetDatabase.LoadAssetAtPath<Sprite>(btnPath);
        Sprite bgSprite = AssetDatabase.LoadAssetAtPath<Sprite>(bgPath);

        string scenePath = "Assets/Scenes/Menu.unity";
        var scene = EditorSceneManager.OpenScene(scenePath);

        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();
        if (canvas == null) return;

        // 2. Set Background Image
        GameObject bgObj = GameObject.Find("SketchBackground");
        if (bgObj == null) {
            bgObj = new GameObject("SketchBackground");
            bgObj.transform.SetParent(canvas.transform, false);
            bgObj.transform.SetAsFirstSibling();
        }
        Image bgImg = bgObj.GetComponent<Image>();
        if (bgImg == null) bgImg = bgObj.AddComponent<Image>();
        bgImg.sprite = bgSprite;
        bgImg.color = Color.white;
        
        RectTransform rtBg = bgObj.GetComponent<RectTransform>();
        rtBg.anchorMin = Vector2.zero;
        rtBg.anchorMax = Vector2.one;
        rtBg.sizeDelta = Vector2.zero;
        rtBg.anchoredPosition = Vector2.zero;

        // 3. Round the Buttons
        GameObject startMenu = GameObject.Find("StartMenu");
        if (startMenu != null) {
            foreach (var btn in startMenu.GetComponentsInChildren<Button>(true)) {
                Image btnImg = btn.GetComponent<Image>();
                if (btnImg != null) {
                    btnImg.sprite = btnSprite;
                    btnImg.type = Image.Type.Sliced; // Best for rounded corners
                    // Create 9-slice if not set, though manual settings are needed for perfect.
                    // For now, simple Sliced is okay.
                    btnImg.color = new Color(1, 1, 1, 0.9f); // Semi-transparent paper
                }
            }
        }

        // 4. Style the Text (White as requested)
        foreach (var tmp in canvas.GetComponentsInChildren<TextMeshProUGUI>()) {
            tmp.color = Color.white;
            tmp.fontStyle = FontStyles.Bold;
            // Add outline for readability
            tmp.outlineWidth = 0.2f;
            tmp.outlineColor = Color.black;
            EditorUtility.SetDirty(tmp);
        }

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        Debug.Log("V4 Sketchy Styling Applied Successfully!");
    }

    private static void ConfigureAsSprite(string path)
    {
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer != null) {
            bool needsReimport = false;
            if (importer.textureType != TextureImporterType.Sprite) {
                importer.textureType = TextureImporterType.Sprite;
                needsReimport = true;
            }
            // Set 9-slice border (20px is safe for my generated art)
            if (importer.spriteBorder == Vector4.zero && path.Contains("Button")) {
                importer.spriteBorder = new Vector4(20, 20, 20, 20);
                needsReimport = true;
            }
            if (needsReimport) {
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
        }
    }
}
