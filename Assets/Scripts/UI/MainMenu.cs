using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject creditsPanel;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        } );
        
        creditsButton.onClick.AddListener(() =>
        {
            ShowCreditsPanel();
        });
        
        quitButton.onClick.AddListener(Application.Quit);
        Time.timeScale = 1f;
    }

    private void Start()
    {
        HideCreditsPanel();
    }

    private void ShowCreditsPanel()
    {
        creditsPanel.SetActive(true);
    }

    private void HideCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }
}
