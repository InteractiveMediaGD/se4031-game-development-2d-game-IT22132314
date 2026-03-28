using UnityEngine;
using TMPro; // Assuming you use TextMeshPro for UI

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [Header("UI Reference")]
    public TMP_Text scoreText; // Drag your Score Text UI element here
    
    private int currentScore = 0;

    void Awake()
    {
        // Setup Singleton so we can access it from anywhere!
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Continue score tracking between levels
        } else {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Find the new UI text whenever a level loads!
        GameObject textObj = GameObject.Find("Score Text");
        if (textObj != null) 
        {
            scoreText = textObj.GetComponent<TMP_Text>();
            UpdateScoreUI();
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void RepositionForEndScreen()
    {
        if (scoreText != null)
        {
            RectTransform rect = scoreText.GetComponent<RectTransform>();
            if (rect != null)
            {
                // Anchor to Top Center instead of Middle to avoid hitting menu buttons
                rect.anchorMin = new Vector2(0.5f, 1f);
                rect.anchorMax = new Vector2(0.5f, 1f);
                rect.pivot = new Vector2(0.5f, 1f);
                rect.anchoredPosition = new Vector2(0, -140f); // Position right under the 'GAME OVER' text
                
                // Increase size for end screen visibility!
                scoreText.fontSize = 52;
                scoreText.alignment = TextAlignmentOptions.Center;
            }
        }
    }
}
