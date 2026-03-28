using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject ControllersUI;
    public GameObject StartMenuUI;
    public Button FirstActiveButton;

    public void PlayGameScene()
    {
        SceneManager.LoadScene("LevelProgressScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadControllersScreen()
    {
        ControllersUI.SetActive(true);
        StartMenuUI.SetActive(false);
    }
    
    void OnEnable()
    {
        SetFirstSelectedButton();
    }

    private void SetFirstSelectedButton()
    {
        FirstActiveButton.Select();
        FirstActiveButton.OnSelect(null);
    }
}
