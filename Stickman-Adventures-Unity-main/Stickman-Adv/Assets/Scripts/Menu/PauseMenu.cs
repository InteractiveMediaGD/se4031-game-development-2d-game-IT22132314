using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
   public static bool IsGamePaused = false;
   public GameObject PauseMenuUI;
   public GameObject ControllersUI;
   public MenuManager MenuManager;

   public Button FirstActiveButton;

    void OnEnable()
    {
        SetFirstSelectedButton();
    }

    public void OpenControllersMenu()
    {
        ControllersUI.SetActive(true);
        PauseMenuUI.SetActive(false);
    }
    private void SetFirstSelectedButton()
    {
        FirstActiveButton.Select();
        FirstActiveButton.OnSelect(null);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
        PauseMenuUI.SetActive(false);
        LevelStateHolder.ResetLevel();     
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
