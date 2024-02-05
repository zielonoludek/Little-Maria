using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private bool _isGamePaused;
    public static Action OnGamePaused;
    public static Action OnGameUnpaused;

    [SerializeField] private Image imageSlot;
    
    private void Awake() => Instance = this;
    private void Start()
    {
        _isGamePaused = false;
        imageSlot.enabled = false;
    }
    public void UpdateItemUIPanel(Sprite itemImage)
    {
        imageSlot.sprite = itemImage;
        imageSlot.enabled = true;
    }
    public void TogglePauseMenu()
    {
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
        }
    }
}
