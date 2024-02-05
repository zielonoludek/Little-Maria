using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private GameObject creditsPanel;
    private void Awake()
    {
        playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        creditsButton.onClick.AddListener(() => creditsPanel.SetActive(true));
        backToMainMenuButton.onClick.AddListener(() => creditsPanel.SetActive(false));

        quitButton.onClick.AddListener(Application.Quit);
        Time.timeScale = 1f;
    }
    
    private void Start()
    {
        creditsPanel.SetActive(false);
    }
}
