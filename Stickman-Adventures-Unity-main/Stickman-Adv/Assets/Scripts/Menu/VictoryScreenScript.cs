using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreenScript : MonoBehaviour
{
    public Button FirstActiveButton;

    public void LoadGameCoverScene()
    {
        LevelStateHolder.ResetLevel();
        SceneManager.LoadScene("GameCover");
    }

    void OnEnable()
    {
        SetFirstSelectedButton();
        
        // Reposition score to avoid overlapping headings
        if (ScoreManager.Instance != null) {
            ScoreManager.Instance.RepositionForEndScreen();
        }
    }

    private void SetFirstSelectedButton()
    {
        FirstActiveButton.Select();
        FirstActiveButton.OnSelect(null);
    }
}
