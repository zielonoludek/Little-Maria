using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            UIManager.Instance.TogglePauseMenu();
        } );
        
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        } );
    }
    
    private void Start()
    {
        UIManager.OnGamePaused += GamePaused;
        UIManager.OnGameUnpaused += GameUnpaused;
        
        Hide();
    }

    private void OnDestroy()
    {
        UIManager.OnGamePaused -= GamePaused;
        UIManager.OnGameUnpaused -= GameUnpaused;
    }

    private void GamePaused()
    {
        Show();
    }

    private void GameUnpaused()
    {
        Hide();
    }
    
    private void Show()
    {
        gameObject.SetActive(true);   
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
