using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{


    public static MenuScript instance { get; private set; }
    [SerializeField] private RectTransform PauseMenu;
    public bool IsPaused { get; set; }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one UI Script in the scene.");
        }
        instance = this;

        PauseMenu.gameObject.SetActive(false);
        IsPaused = false;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Unpause();
    }

    public void Pause()
    {
        IsPaused = true;
        print("Pause");
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    public void Unpause()
    {
        IsPaused = false;
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelectFirstChoice());
    }


    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(PauseMenu.GetChild(0).gameObject);
    }
}
